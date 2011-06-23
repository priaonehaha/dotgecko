using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotGecko.Gecko.Tests
{
	[TestClass]
	public class DeclarationsTest
	{
		public TestContext TestContext { get; set; }

		[ClassInitialize]
		public static void LoadDeclarations(TestContext testContext)
		{
			const String interopAssemblyName = "DotGecko.Gecko.Interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0ba2ce6fc89dc016";
			Assembly interopAssembly = Assembly.ReflectionOnlyLoad(interopAssemblyName);
			Assert.IsNotNull(interopAssembly);

			Type[] interopTypes = interopAssembly.GetTypes();
			ms_Interfaces = interopTypes
				.Where(type => type.IsInterface && (type.Name.StartsWith("nsI") || type.Name.StartsWith("mozI") || type.Name.StartsWith("imgI")))
				.ToList().AsReadOnly();
			ms_ConstClasses = interopTypes
				.Where(type => type.IsClass && (type.Name.StartsWith("nsI") || type.Name.StartsWith("mozI") || type.Name.StartsWith("imgI")) && type.Name.EndsWith(ConstClassSuffix))
				.ToList().AsReadOnly();

			IEnumerable<Type> unknownTypes = interopTypes.Except(ms_Interfaces).Except(ms_ConstClasses);
			foreach (Type unknownType in unknownTypes)
			{
				testContext.WriteLine("Unknown type: {0} ({1})", unknownType.Name, unknownType.FullName);
			}
		}

		[TestMethod]
		public void CheckInterfaces()
		{
			foreach (Type interfaceType in ms_Interfaces)
			{
				Assert.AreEqual(InteropNamespace, interfaceType.Namespace, "Interface {0} declared in wrong namespace", interfaceType.Name);
				Assert.IsTrue(interfaceType.IsPublic, "Interface {0} must be public", interfaceType.Name);

				IList<CustomAttributeData> attributes = interfaceType.GetCustomAttributesData();

				Int32 comImportCount = attributes.Count(data => data.Constructor.DeclaringType == typeof(ComImportAttribute));
				Assert.AreEqual(1, comImportCount, "Apply ComImportAttribute to {0} interface", interfaceType.Name);

				Int32 guidCount = attributes.Count(data => data.Constructor.DeclaringType == typeof(GuidAttribute));
				Assert.AreEqual(1, guidCount, "Apply GuidAttribute to {0} interface", interfaceType.Name);

				Func<CustomAttributeData, Boolean> interfaceTypeAttrPredicate =
					data => data.Constructor.DeclaringType == typeof(InterfaceTypeAttribute);
				Int32 interfaceTypeCount = attributes.Count(interfaceTypeAttrPredicate);
				Assert.AreEqual(1, interfaceTypeCount, "Apply InterfaceTypeAttribute to {0} interface", interfaceType.Name);

				CustomAttributeData interfaceTypeAttribute = attributes.Single(interfaceTypeAttrPredicate);
				CustomAttributeTypedArgument comInterfaceTypeData = interfaceTypeAttribute.ConstructorArguments[0];
				Assert.AreEqual(ComInterfaceType.InterfaceIsIUnknown, (ComInterfaceType)comInterfaceTypeData.Value, "Invalid ComInterfaceType applied to {0} interface", interfaceType.Name);
			}
		}

		[TestMethod]
		public void CheckInterfaceMembers()
		{
			foreach (Type interfaceType in ms_Interfaces)
			{
				MethodInfo[] interfaceMethods = interfaceType
					.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
					.OrderBy(Marshal.GetComSlotForMethodInfo)
					.ToArray();
				foreach (MethodInfo interfaceMethod in interfaceMethods)
				{
					String methodName = interfaceMethod.Name;
					Int32 firstSymbolIndex = interfaceMethod.IsSpecialName ? 4 : 0;
					Assert.IsTrue(Char.IsUpper(methodName, firstSymbolIndex), "First name symbol must be upper case ({0}.{1})", interfaceType.Name, methodName);
				}
			}
		}

		[TestMethod]
		public void CheckInheritedInterfaceMembers()
		{
			foreach (Type interfaceType in ms_Interfaces)
			{
				Type baseInterface = GetBaseInterface(interfaceType);
				if (baseInterface == null)
				{
					continue;
				}

				MethodInfo[] interfaceMethods = interfaceType
					.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
					.OrderBy(Marshal.GetComSlotForMethodInfo)
					.ToArray();
				MethodInfo[] baseMethods = baseInterface
					.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
					.OrderBy(Marshal.GetComSlotForMethodInfo)
					.ToArray();
				Assert.IsTrue(interfaceMethods.Length >= baseMethods.Length, "Not all members from {0} interface declared in {1} interface", baseInterface.Name, interfaceType.Name);
				for (Int32 i = 0; i < baseMethods.Length; ++i)
				{
					MethodInfo baseMethod = baseMethods[i];
					MethodInfo interfaceMethod = interfaceMethods[i];
					Boolean methodsEqual = MethodsEqual(baseMethod, interfaceMethod);
					Assert.IsTrue(methodsEqual, "Invalid method declaration in {0}.\n\"{1}\" is expected, but \"{2}\" found.", interfaceType.Name, baseMethod.Name, interfaceMethod.Name);
					Assert.IsTrue(interfaceMethod.IsHideBySig, "Method {0}.{1} must hide base method", interfaceType.Name, interfaceMethod.Name);
				}
			}
		}

		[TestMethod]
		public void CheckConstClasses()
		{
			foreach (Type constClass in ms_ConstClasses)
			{
				String constClassName = constClass.Name;
				Assert.AreEqual(InteropNamespace, constClass.Namespace, "Class {0} declared in wrong namespace", constClassName);
				Assert.IsTrue(constClass.IsPublic, "Class {0} must be public", constClassName);
				Assert.IsTrue(constClass.IsAbstract && constClass.IsSealed, "Class {0} must be static", constClassName);

				String constInterfaceName = constClass.Name.Substring(0, constClassName.Length - ConstClassSuffix.Length);
				Boolean hasCorrespondingInterface = ms_Interfaces.Any(type => type.Name == constInterfaceName);
				Assert.IsTrue(hasCorrespondingInterface, "There is no corresponding interface for {0} class", constClassName);
			}
		}

		[TestMethod]
		public void CheckConstants()
		{
			foreach (Type constClass in ms_ConstClasses)
			{
				MemberInfo[] members = constClass.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				Assert.IsTrue(members.Length > 0, "Class {0} must have a members", constClass.Name);

				foreach (MemberInfo member in members)
				{
					Assert.IsTrue(member is FieldInfo, "Member {0}.{1} is not a public constant", constClass.Name, member.Name);
					var field = (FieldInfo)member;
					Assert.IsTrue(field.IsStatic && field.IsLiteral, "Field {0}.{1} must be a constant", constClass.Name, field.Name);
					Assert.IsTrue(field.IsPublic, "Constant {0}.{1} must be public", constClass.Name, field.Name);
				}
			}
		}

		private static Type GetBaseInterface(Type interfaceType)
		{
			Type[] baseInterfaces = interfaceType.GetInterfaces();
			if (baseInterfaces.Length > 0)
			{
				IEnumerable<Type> filteredInteraces = baseInterfaces
					.Aggregate<Type, IEnumerable<Type>>(
						baseInterfaces,
						(current, baseInterface) => current.Except(baseInterface.GetInterfaces()));
				return filteredInteraces.Single();
			}
			return null;
		}

		private static Boolean MethodsEqual(MethodInfo aMethod, MethodInfo bMethod)
		{
			//TODO: Compare all arguments, custom attributes, etc.
			return aMethod.ToString() == bMethod.ToString();
		}

		private const String InteropNamespace = "DotGecko.Gecko.Interop";
		private const String ConstClassSuffix = "Constants";
		private static IList<Type> ms_Interfaces;
		private static IList<Type> ms_ConstClasses;
	}
}
