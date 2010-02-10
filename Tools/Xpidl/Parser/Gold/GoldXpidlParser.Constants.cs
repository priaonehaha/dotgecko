namespace Xpidl.Parser.Gold
{
	internal sealed partial class GoldXpidlParser
	{
		private enum SymbolConstants
		{
			SYMBOL_EOF = 0, // (EOF)
			SYMBOL_ERROR = 1, // (Error)
			SYMBOL_WHITESPACE = 2, // (Whitespace)
			SYMBOL_COMMENTEND = 3, // (Comment End)
			SYMBOL_COMMENTLINE = 4, // (Comment Line)
			SYMBOL_COMMENTSTART = 5, // (Comment Start)
			SYMBOL_MINUS = 6, // '-'
			SYMBOL_QUOTE = 7, // '"'
			SYMBOL_NUMINCLUDE = 8, // '#include'
			SYMBOL_AMP = 9, // '&'
			LParan = 10, // '('
			RParan = 11, // ')'
			SYMBOL_TIMES = 12, // '*'
			Comma = 13, // ','
			SYMBOL_DIV = 14, // '/'
			Colon = 15, // ':'
			Semicolon = 16, // ';'
			LBracket = 17, // '['
			RBracket = 18, // ']'
			SYMBOL_CARET = 19, // '^'
			LBrace = 20, // '{'
			SYMBOL_PIPE = 21, // '|'
			RBrace = 22, // '}'
			SYMBOL_TILDE = 23, // '~'
			SYMBOL_PLUS = 24, // '+'
			SYMBOL_LT = 25, // '<'
			SYMBOL_LTLT = 26, // '<<'
			SYMBOL_EQ = 27, // '='
			SYMBOL_GT = 28, // '>'
			SYMBOL_GTGT = 29, // '>>'
			SYMBOL_ACSTRING = 30, // ACString
			SYMBOL_ARRAY = 31, // array
			SYMBOL_ASTRING = 32, // astring
			SYMBOL_ATTRIBUTE = 33, // attribute
			SYMBOL_AUTF8STRING = 34, // 'AUTF8String'
			SYMBOL_BINARYNAME = 35, // binaryname
			SYMBOL_BOOLEAN = 36, // boolean
			SYMBOL_CHAR = 37, // char
			SYMBOL_CONST = 38, // const
			SYMBOL_CSTRING = 39, // cstring
			DecLiteral = 40, // dec-literal
			SYMBOL_DOMSTRING = 41, // domstring
			SYMBOL_DOUBLE = 42, // double
			SYMBOL_FILENAMEMINUSLITERAL = 43, // filename-literal
			SYMBOL_FLOAT = 44, // float
			SYMBOL_FUNCTION = 45, // function
			HexLiteral = 46, // hex-literal
			SYMBOL_IID_IS = 47, // 'iid_is'
			SYMBOL_IN = 48, // in
			SYMBOL_INOUT = 49, // inout
			Interface = 50, // interface
			SYMBOL_LONG = 51, // long
			SYMBOL_LONGLONG = 52, // 'long long'
			SYMBOL_NATIVE = 53, // native
			SYMBOL_NOSCRIPT = 54, // noscript
			SYMBOL_NOTXPCOM = 55, // notxpcom
			SYMBOL_NSID = 56, // nsid
			SYMBOL_OBJECT = 57, // object
			SYMBOL_OCTET = 58, // octet
			SYMBOL_OPTIONAL = 59, // optional
			SYMBOL_OUT = 60, // out
			SYMBOL_PTR = 61, // ptr
			SYMBOL_RAISES = 62, // raises
			SYMBOL_READONLY = 63, // readonly
			SYMBOL_REF = 64, // ref
			SYMBOL_RETVAL = 65, // retval
			SYMBOL_SCRIPTABLE = 66, // scriptable
			SYMBOL_SHARED = 67, // shared
			SYMBOL_SHORT = 68, // short
			SYMBOL_SIZE_IS = 69, // 'size_is'
			SYMBOL_STRING = 70, // string
			SYMBOL_STRUCT = 71, // struct
			SYMBOL_TYPEDEF = 72, // typedef
			SYMBOL_UNION = 73, // union
			SYMBOL_UNSIGNED = 74, // unsigned
			SYMBOL_UNSIGNEDLONG = 75, // 'unsigned long'
			SYMBOL_UNSIGNEDLONGLONG = 76, // 'unsigned long long'
			SYMBOL_UNSIGNEDSHORT = 77, // 'unsigned short'
			SYMBOL_UTF8STRING = 78, // 'utf8string'
			SYMBOL_UUID = 79, // uuid
			SYMBOL_UUIDMINUSLITERAL = 80, // uuid-literal
			SYMBOL_VOID = 81, // void
			SYMBOL_WCHAR = 82, // wchar
			SYMBOL_WSTRING = 83, // wstring
			XpidlId = 84, // xpidl-id
			SYMBOL_ATTRIBUTEMINUSNAME = 85, // <attribute-name>
			SYMBOL_XPIDL = 86, // <xpidl>
			SYMBOL_XPIDLMINUSATTRIBUTE = 87, // <xpidl-attribute>
			SYMBOL_XPIDLMINUSCONSTANT = 88, // <xpidl-constant>
			SYMBOL_XPIDLMINUSEXPRESSION = 89, // <xpidl-expression>
			SYMBOL_XPIDLMINUSEXPRESSIONMINUSADD = 90, // <xpidl-expression-add>
			SYMBOL_XPIDLMINUSEXPRESSIONMINUSAND = 91, // <xpidl-expression-and>
			SYMBOL_XPIDLMINUSEXPRESSIONMINUSMULT = 92, // <xpidl-expression-mult>
			SYMBOL_XPIDLMINUSEXPRESSIONMINUSOPERAND = 93, // <xpidl-expression-operand>
			SYMBOL_XPIDLMINUSEXPRESSIONMINUSOR = 94, // <xpidl-expression-or>
			SYMBOL_XPIDLMINUSEXPRESSIONMINUSSHIFT = 95, // <xpidl-expression-shift>
			SYMBOL_XPIDLMINUSEXPRESSIONMINUSUNARY = 96, // <xpidl-expression-unary>
			SYMBOL_XPIDLMINUSEXPRESSIONMINUSXOR = 97, // <xpidl-expression-xor>
			SYMBOL_XPIDLMINUSFORWARDMINUSDECLARATION = 98, // <xpidl-forward-declaration>
			SYMBOL_XPIDLMINUSINCLUDE = 99, // <xpidl-include>
			SYMBOL_XPIDLMINUSINTERFACE = 100, // <xpidl-interface>
			SYMBOL_XPIDLMINUSINTERFACEMINUSMEMBER = 101, // <xpidl-interface-member>
			SYMBOL_XPIDLMINUSINTERFACEMINUSMEMBERS = 102, // <xpidl-interface-members>
			SYMBOL_XPIDLMINUSINTERFACEMINUSMODIFIER = 103, // <xpidl-interface-modifier>
			SYMBOL_XPIDLMINUSINTERFACEMINUSMODIFIERSMINUSDECL = 104, // <xpidl-interface-modifiers-decl>
			SYMBOL_XPIDLMINUSINTERFACEMINUSMODIFIERSMINUSLIST = 105, // <xpidl-interface-modifiers-list>
			SYMBOL_XPIDLMINUSINTERFACEMINUSUUID = 106, // <xpidl-interface-uuid>
			SYMBOL_XPIDLMINUSITEM = 107, // <xpidl-item>
			SYMBOL_XPIDLMINUSITEMS = 108, // <xpidl-items>
			SYMBOL_XPIDLMINUSMETHOD = 109, // <xpidl-method>
			SYMBOL_XPIDLMINUSMETHODMINUSEXCEPTIONSMINUSLIST = 110, // <xpidl-method-exceptions-list>
			SYMBOL_XPIDLMINUSMETHODMINUSMODIFIER = 111, // <xpidl-method-modifier>
			SYMBOL_XPIDLMINUSMETHODMINUSMODIFIERSMINUSDECL = 112, // <xpidl-method-modifiers-decl>
			SYMBOL_XPIDLMINUSMETHODMINUSMODIFIERSMINUSLIST = 113, // <xpidl-method-modifiers-list>
			SYMBOL_XPIDLMINUSMETHODMINUSRAISES = 114, // <xpidl-method-raises>
			SYMBOL_XPIDLMINUSNATIVEMINUSTYPE = 115, // <xpidl-native-type>
			SYMBOL_XPIDLMINUSNATIVEMINUSTYPEMINUSDECL = 116, // <xpidl-native-type-decl>
			SYMBOL_XPIDLMINUSNATIVEMINUSTYPEMINUSLIST = 117, // <xpidl-native-type-list>
			SYMBOL_XPIDLMINUSNATIVEMINUSTYPEMINUSMODIFIER = 118, // <xpidl-native-type-modifier>
			SYMBOL_XPIDLMINUSNATIVEMINUSTYPEMINUSMODIFIERSMINUSDECL = 119, // <xpidl-native-type-modifiers-decl>
			SYMBOL_XPIDLMINUSNATIVEMINUSTYPEMINUSMODIFIERSMINUSLIST = 120, // <xpidl-native-type-modifiers-list>
			SYMBOL_XPIDLMINUSNATIVEMINUSTYPEMINUSNAME = 121, // <xpidl-native-type-name>
			SYMBOL_XPIDLMINUSPARAM = 122, // <xpidl-param>
			SYMBOL_XPIDLMINUSPARAMMINUSMODIFIER = 123, // <xpidl-param-modifier>
			SYMBOL_XPIDLMINUSPARAMMINUSMODIFIERSMINUSDECL = 124, // <xpidl-param-modifiers-decl>
			SYMBOL_XPIDLMINUSPARAMMINUSMODIFIERSMINUSLIST = 125, // <xpidl-param-modifiers-list>
			SYMBOL_XPIDLMINUSPARAMMINUSNAME = 126, // <xpidl-param-name>
			SYMBOL_XPIDLMINUSPARAMSMINUSLIST = 127, // <xpidl-params-list>
			SYMBOL_XPIDLMINUSPARAMMINUSTYPE = 128, // <xpidl-param-type>
			SYMBOL_XPIDLMINUSTYPE = 129, // <xpidl-type>
			SYMBOL_XPIDLMINUSTYPEDEF = 130  // <xpidl-typedef>
		};

		private enum RuleConstants
		{
			// Xpidl root items
			Xpidl = 0, // <xpidl> ::= <xpidl-items>
			XpidlItems1 = 1, // <xpidl-items> ::= <xpidl-items> <xpidl-item>
			XpidlItems2 = 2, // <xpidl-items> ::= <xpidl-item>
			XpidlItem1 = 3, // <xpidl-item> ::= <xpidl-typedef>
			XpidlItem2 = 4, // <xpidl-item> ::= <xpidl-include>
			XpidlItem3 = 5, // <xpidl-item> ::= <xpidl-forward-declaration>
			XpidlItem4 = 6, // <xpidl-item> ::= <xpidl-native-type>
			XpidlItem5 = 7, // <xpidl-item> ::= <xpidl-interface>

			XpidlTypeDef = 8, // <xpidl-typedef> ::= typedef <xpidl-type> xpidl-id ';'
			XpidlInclude = 9, // <xpidl-include> ::= '#include' '"' filename-literal '"'
			XpidlForwardDeclaration = 10, // <xpidl-forward-declaration> ::= interface xpidl-id ';'
			XpidlNativeType1 = 11, // <xpidl-native-type> ::= native xpidl-id '(' <xpidl-native-type-list> ')' ';'
			XpidlNativeType2 = 12, // <xpidl-native-type> ::= <xpidl-native-type-modifiers-decl> native xpidl-id '(' <xpidl-native-type-list> ')' ';'

			RULE_XPIDLNATIVETYPELIST = 13, // <xpidl-native-type-list> ::= <xpidl-native-type-list> <xpidl-native-type-decl>
			RULE_XPIDLNATIVETYPELIST2 = 14, // <xpidl-native-type-list> ::= <xpidl-native-type-decl>
			RULE_XPIDLNATIVETYPEDECL = 15, // <xpidl-native-type-decl> ::= <xpidl-native-type-name>
			RULE_XPIDLNATIVETYPEDECL_LT_XPIDLMINUSID_GT = 16, // <xpidl-native-type-decl> ::= <xpidl-native-type-name> '<' xpidl-id '>'
			RULE_XPIDLNATIVETYPEDECL_AMP = 17, // <xpidl-native-type-decl> ::= <xpidl-native-type-name> '&'
			RULE_XPIDLNATIVETYPEDECL_TIMES = 18, // <xpidl-native-type-decl> ::= <xpidl-native-type-name> '*'
			RULE_XPIDLNATIVETYPEDECL_TIMES_TIMES = 19, // <xpidl-native-type-decl> ::= <xpidl-native-type-name> '*' '*'
			RULE_XPIDLNATIVETYPENAME_VOID = 20, // <xpidl-native-type-name> ::= void
			RULE_XPIDLNATIVETYPENAME_CONST = 21, // <xpidl-native-type-name> ::= const
			RULE_XPIDLNATIVETYPENAME_STRUCT = 22, // <xpidl-native-type-name> ::= struct
			RULE_XPIDLNATIVETYPENAME_UNION = 23, // <xpidl-native-type-name> ::= union
			RULE_XPIDLNATIVETYPENAME_UNSIGNED = 24, // <xpidl-native-type-name> ::= unsigned
			RULE_XPIDLNATIVETYPENAME_CHAR = 25, // <xpidl-native-type-name> ::= char
			RULE_XPIDLNATIVETYPENAME_XPIDLMINUSID = 26, // <xpidl-native-type-name> ::= xpidl-id
			RULE_XPIDLNATIVETYPEMODIFIERSDECL_LBRACKET_RBRACKET = 27, // <xpidl-native-type-modifiers-decl> ::= '[' <xpidl-native-type-modifiers-list> ']'
			RULE_XPIDLNATIVETYPEMODIFIERSLIST_COMMA = 28, // <xpidl-native-type-modifiers-list> ::= <xpidl-native-type-modifiers-list> ',' <xpidl-native-type-modifier>
			RULE_XPIDLNATIVETYPEMODIFIERSLIST = 29, // <xpidl-native-type-modifiers-list> ::= <xpidl-native-type-modifier>
			RULE_XPIDLNATIVETYPEMODIFIER_REF = 30, // <xpidl-native-type-modifier> ::= ref
			RULE_XPIDLNATIVETYPEMODIFIER_PTR = 31, // <xpidl-native-type-modifier> ::= ptr
			RULE_XPIDLNATIVETYPEMODIFIER_NSID = 32, // <xpidl-native-type-modifier> ::= nsid
			RULE_XPIDLNATIVETYPEMODIFIER_DOMSTRING = 33, // <xpidl-native-type-modifier> ::= domstring
			RULE_XPIDLNATIVETYPEMODIFIER_UTF8STRING = 34, // <xpidl-native-type-modifier> ::= 'utf8string'
			RULE_XPIDLNATIVETYPEMODIFIER_CSTRING = 35, // <xpidl-native-type-modifier> ::= cstring
			RULE_XPIDLNATIVETYPEMODIFIER_ASTRING = 36, // <xpidl-native-type-modifier> ::= astring

			XpidlInterface1 = 37, // <xpidl-interface> ::= <xpidl-interface-modifiers-decl> interface xpidl-id ':' xpidl-id '{' <xpidl-interface-members> '}' ';'
			XpidlInterface2 = 38, // <xpidl-interface> ::= <xpidl-interface-modifiers-decl> interface xpidl-id ':' xpidl-id '{' '}' ';'
			XpidlInterface3 = 39, // <xpidl-interface> ::= <xpidl-interface-modifiers-decl> interface xpidl-id '{' <xpidl-interface-members> '}' ';'
			XpidlInterface4 = 40, // <xpidl-interface> ::= <xpidl-interface-modifiers-decl> interface xpidl-id '{' '}' ';'

			// Interface modifiers declaration
			XpidlInterfaceModifiersDecl1 = 41, // <xpidl-interface-modifiers-decl> ::= '[' <xpidl-interface-modifiers-list> ',' <xpidl-interface-uuid> ']'
			XpidlInterfaceModifiersDecl2 = 42, // <xpidl-interface-modifiers-decl> ::= '[' <xpidl-interface-uuid> ']'
			XpidlInterfaceModifiersList1 = 43, // <xpidl-interface-modifiers-list> ::= <xpidl-interface-modifiers-list> ',' <xpidl-interface-modifier>
			XpidlInterfaceModifiersList2 = 44, // <xpidl-interface-modifiers-list> ::= <xpidl-interface-modifier>

			// Interface modifiers
			XpidlInterfaceModifierScriptable = 45, // <xpidl-interface-modifier> ::= scriptable
			XpidlInterfaceModifierFunction = 46, // <xpidl-interface-modifier> ::= function
			XpidlInterfaceModifierObject = 47, // <xpidl-interface-modifier> ::= object
			XpidlInterfaceModifierNotXpcom = 48, // <xpidl-interface-modifier> ::= notxpcom
			XpidlInterfaceModifierNoScript = 49, // <xpidl-interface-modifier> ::= noscript

			// Interface UUID
			XpidlInterfaceUuid = 50, // <xpidl-interface-uuid> ::= uuid '(' uuid-literal ')'

			// Interface members list
			XpidlInterfaceMembers1 = 51, // <xpidl-interface-members> ::= <xpidl-interface-members> <xpidl-interface-member>
			XpidlInterfaceMembers2 = 52, // <xpidl-interface-members> ::= <xpidl-interface-member>

			// Interface members
			XpidlInterfaceMemberConstant = 53,  // <xpidl-interface-member> ::= <xpidl-constant>
			XpidlInterfaceMemberAttribute = 54, // <xpidl-interface-member> ::= <xpidl-attribute>
			XpidlInterfaceMemberMethod = 55,    // <xpidl-interface-member> ::= <xpidl-method>

			XpidlConstant = 56, // <xpidl-constant> ::= const <xpidl-type> xpidl-id '=' <xpidl-expression> ';'

			XpidlExpression = 57, // <xpidl-expression> ::= <xpidl-expression-or>
			XpidlExpressionOr1 = 58, // <xpidl-expression-or> ::= <xpidl-expression-or> '|' <xpidl-expression-xor>
			XpidlExpressionOr2 = 59, // <xpidl-expression-or> ::= <xpidl-expression-xor>
			XpidlExpressionXor1 = 60, // <xpidl-expression-xor> ::= <xpidl-expression-xor> '^' <xpidl-expression-and>
			XpidlExpressionXor2 = 61, // <xpidl-expression-xor> ::= <xpidl-expression-and>
			XpidlExpressionAnd1 = 62, // <xpidl-expression-and> ::= <xpidl-expression-and> '&' <xpidl-expression-shift>
			XpidlExpressionAnd2 = 63, // <xpidl-expression-and> ::= <xpidl-expression-shift>
			XpidlExpressionShift1 = 64, // <xpidl-expression-shift> ::= <xpidl-expression-shift> '<<' <xpidl-expression-add>
			XpidlExpressionShift2 = 65, // <xpidl-expression-shift> ::= <xpidl-expression-shift> '>>' <xpidl-expression-add>
			XpidlExpressionShift3 = 66, // <xpidl-expression-shift> ::= <xpidl-expression-add>
			XpidlExpressionAdd1 = 67, // <xpidl-expression-add> ::= <xpidl-expression-add> '+' <xpidl-expression-mult>
			XpidlExpressionAdd2 = 68, // <xpidl-expression-add> ::= <xpidl-expression-add> '-' <xpidl-expression-mult>
			XpidlExpressionAdd3 = 69, // <xpidl-expression-add> ::= <xpidl-expression-mult>
			XpidlExpressionMult1 = 70, // <xpidl-expression-mult> ::= <xpidl-expression-mult> '*' <xpidl-expression-unary>
			XpidlExpressionMult2 = 71, // <xpidl-expression-mult> ::= <xpidl-expression-mult> '/' <xpidl-expression-unary>
			XpidlExpressionMult3 = 72, // <xpidl-expression-mult> ::= <xpidl-expression-unary>
			XpidlExpressionUnary1 = 73, // <xpidl-expression-unary> ::= '-' <xpidl-expression-operand>
			XpidlExpressionUnary2 = 74, // <xpidl-expression-unary> ::= '~' <xpidl-expression-operand>
			XpidlExpressionUnary3 = 75, // <xpidl-expression-unary> ::= <xpidl-expression-operand>
			XpidlExpressionOperand1 = 76, // <xpidl-expression-operand> ::= dec-literal
			XpidlExpressionOperand2 = 77, // <xpidl-expression-operand> ::= hex-literal
			XpidlExpressionOperand3 = 78, // <xpidl-expression-operand> ::= xpidl-id
			XpidlExpressionOperand4 = 79, // <xpidl-expression-operand> ::= '(' <xpidl-expression> ')'

			XpidlAttribute1 = 80, // <xpidl-attribute> ::= attribute <xpidl-type> <attribute-name> ';'
			XpidlAttribute2 = 81, // <xpidl-attribute> ::= readonly attribute <xpidl-type> <attribute-name> ';'
			XpidlAttribute3 = 82, // <xpidl-attribute> ::= <xpidl-method-modifiers-decl> attribute <xpidl-type> <attribute-name> ';'
			XpidlAttribute4 = 83, // <xpidl-attribute> ::= <xpidl-method-modifiers-decl> readonly attribute <xpidl-type> <attribute-name> ';'

			XpidlAttributeName1 = 84, // <attribute-name> ::= ref
			XpidlAttributeName2 = 85, // <attribute-name> ::= object
			XpidlAttributeName3 = 86, // <attribute-name> ::= xpidl-id

			XpidlMethod1 = 87, // <xpidl-method> ::= <xpidl-type> xpidl-id '(' <xpidl-params-list> ')' ';'
			XpidlMethod2 = 88, // <xpidl-method> ::= <xpidl-type> xpidl-id '(' ')' ';'
			XpidlMethod3 = 89, // <xpidl-method> ::= <xpidl-method-modifiers-decl> <xpidl-type> xpidl-id '(' <xpidl-params-list> ')' ';'
			XpidlMethod4 = 90, // <xpidl-method> ::= <xpidl-method-modifiers-decl> <xpidl-type> xpidl-id '(' ')' ';'
			XpidlMethod5 = 91, // <xpidl-method> ::= <xpidl-type> xpidl-id '(' <xpidl-params-list> ')' <xpidl-method-raises> ';'
			XpidlMethod6 = 92, // <xpidl-method> ::= <xpidl-type> xpidl-id '(' ')' <xpidl-method-raises> ';'
			XpidlMethod7 = 93, // <xpidl-method> ::= <xpidl-method-modifiers-decl> <xpidl-type> xpidl-id '(' <xpidl-params-list> ')' <xpidl-method-raises> ';'
			XpidlMethod8 = 94, // <xpidl-method> ::= <xpidl-method-modifiers-decl> <xpidl-type> xpidl-id '(' ')' <xpidl-method-raises> ';'

			XpidlMethodModifiersDecl = 95, // <xpidl-method-modifiers-decl> ::= '[' <xpidl-method-modifiers-list> ']'
			XpidlMethodModifiersList1 = 96, // <xpidl-method-modifiers-list> ::= <xpidl-method-modifiers-list> ',' <xpidl-method-modifier>
			XpidlMethodModifiersList2 = 97, // <xpidl-method-modifiers-list> ::= <xpidl-method-modifier>
			XpidlMethodModifier1 = 98, // <xpidl-method-modifier> ::= noscript
			XpidlMethodModifier2 = 99, // <xpidl-method-modifier> ::= notxpcom
			XpidlMethodModifier3 = 100, // <xpidl-method-modifier> ::= binaryname '(' xpidl-id ')'
			XpidlMethodRaises = 101, // <xpidl-method-raises> ::= raises '(' <xpidl-method-exceptions-list> ')'
			XpidlMethodExceptionsList1 = 102, // <xpidl-method-exceptions-list> ::= <xpidl-method-exceptions-list> ',' xpidl-id
			XpidlMethodExceptionsList2 = 103, // <xpidl-method-exceptions-list> ::= xpidl-id

			XpidlMethodParamsList1 = 104, // <xpidl-params-list> ::= <xpidl-params-list> ',' <xpidl-param>
			XpidlMethodParamsList2 = 105, // <xpidl-params-list> ::= <xpidl-param>

			XpidlMethodParam1 = 106, // <xpidl-param> ::= <xpidl-param-type> <xpidl-type> <xpidl-param-name>
			XpidlMethodParam2 = 107, // <xpidl-param> ::= <xpidl-param-modifiers-decl> <xpidl-param-type> <xpidl-type> <xpidl-param-name>

			XpidlMethodParamModifiersDecl = 108, // <xpidl-param-modifiers-decl> ::= '[' <xpidl-param-modifiers-list> ']'
			XpidlMethodParamModifiersList1 = 109, // <xpidl-param-modifiers-list> ::= <xpidl-param-modifiers-list> ',' <xpidl-param-modifier>
			XpidlMethodParamModifiersList2 = 110, // <xpidl-param-modifiers-list> ::= <xpidl-param-modifier>

			XpidlParamModifierArray = 111,    // <xpidl-param-modifier> ::= array
			XpidlParamModifierSizeIs = 112,   // <xpidl-param-modifier> ::= 'size_is' '(' <xpidl-param-name> ')'
			XpidlParamModifierIidIs = 113,    // <xpidl-param-modifier> ::= 'iid_is' '(' <xpidl-param-name> ')'
			XpidlParamModifierRetVal = 114,   // <xpidl-param-modifier> ::= retval
			XpidlParamModifierConst = 115,    // <xpidl-param-modifier> ::= const
			XpidlParamModifierShared = 116,   // <xpidl-param-modifier> ::= shared
			XpidlParamModifierOptional = 117, // <xpidl-param-modifier> ::= optional

			XpidlParamDirectionIn = 118,    // <xpidl-param-type> ::= in
			XpidlParamDirectionOut = 119,   // <xpidl-param-type> ::= out
			XpidlParamDirectionInOut = 120, // <xpidl-param-type> ::= inout

			XpidlParamName1 = 121, // <xpidl-param-name> ::= ptr
			XpidlParamName2 = 122, // <xpidl-param-name> ::= uuid
			XpidlParamName3 = 123, // <xpidl-param-name> ::= array
			XpidlParamName4 = 124, // <xpidl-param-name> ::= object
			XpidlParamName5 = 125, // <xpidl-param-name> ::= retval
			XpidlParamName6 = 126, // <xpidl-param-name> ::= xpidl-id

			XpidlTypeBoolean = 127,     // <xpidl-type> ::= boolean
			XpidlTypeVoid = 128,        // <xpidl-type> ::= void
			XpidlTypeString = 129,      // <xpidl-type> ::= string
			XpidlTypeOctet = 130,       // <xpidl-type> ::= octet
			XpidlTypeShort = 131,       // <xpidl-type> ::= short
			XpidlTypeLong = 132,        // <xpidl-type> ::= long
			XpidlTypeLongLong = 133,    // <xpidl-type> ::= 'long long'
			XpidlTypeUShort = 134,      // <xpidl-type> ::= 'unsigned short'
			XpidlTypeULong = 135,       // <xpidl-type> ::= 'unsigned long'
			XpidlTypeULongLong = 136,   // <xpidl-type> ::= 'unsigned long long'
			XpidlTypeFloat = 137,       // <xpidl-type> ::= float
			XpidlTypeDouble = 138,      // <xpidl-type> ::= double
			XpidlTypeChar = 139,        // <xpidl-type> ::= char
			XpidlTypeWChar = 140,       // <xpidl-type> ::= wchar
			XpidlTypeWString = 141,     // <xpidl-type> ::= wstring
			XpidlTypeAString = 142,     // <xpidl-type> ::= astring
			XpidlTypeACString = 143,    // <xpidl-type> ::= ACString
			XpidlTypeAUTF8String = 144, // <xpidl-type> ::= 'AUTF8String'
			XpidlTypeDOMString = 145,   // <xpidl-type> ::= domstring
			XpidlTypeId = 146           // <xpidl-type> ::= xpidl-id
		};
	}
}
