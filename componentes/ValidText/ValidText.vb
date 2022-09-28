'----------------------------------------------------------------------------------
' - Author:           - Luis Alberto Ruiz Arauz
' - Date:             - 28/05/2003
'----------------------------------------------------------------------------------
' - Component:        - ValidText
' - Version:          - 1.1
' - Description:      - TextBox with attributes and methods of validation.
'----------------------------------------------------------------------------------
' - Features:         - Valid characters validation.
'                     - Invalid characters validation.
'                     - Letters validation.
'                     - Numbers validation.
'                     - Mask validation (MaskEdit).
'                     - Regular expressions validation.
'                           * Email.
'                           * URL.
'                           * IP.
'                           * Date.
'                           * ZIP code.
'                           * Custom.
'                     - Required field validation.
'----------------------------------------------------------------------------------
Option Strict On

Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Resources
Imports System.Text.RegularExpressions

<ToolboxBitmapAttribute(GetType(ValidText), "ValidText.ico")> _
Public Class ValidText
    Inherits System.Windows.Forms.TextBox

    Private firstTime As Boolean = True
    Private firstLoad As Boolean = True
    Private secondLoad As Boolean = False
    Private thirdLoad As Boolean = False
    Private lastText As String = ""
    Private keyType As String
    Private maskString As String
    Private maskValues As String
    Private maskText As String
    Private maskBool As Boolean = False
    Private validCharBool As Boolean = False
    Private isRequired As Byte = 0
    Private vexit As Boolean = False
    Private WithEvents miForm As Form = MyBase.FindForm
    Private LETTERS As String = "AÁBCDEÉFGHIÍJKLMNÑOÓPQRSTUÚÜVWXYZ"
    Private NUMBERS As String = "1234567890"
    Private EMAIL As String = "^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
    Private IP As String = "(?<First>[01]?\d\d?|2[0-4]\d|25[0-5])\.(?<Second>[01]?\d\d?|2[0-4]\d|25[0-5])\.(?<Third>[01]?\d\d?|2[0-4]\d|25[0-5])\.(?<Fourth>[01]?\d\d?|2[0-4]\d|25[0-5])(?x) "
    Private URL As String = "^(?<proto>\w+)://[^/]+?(?<port>:\d+)?/"
    Private DATEC As String = "(?<Month>\d{1,2})/(?<Day>\d{1,2})/(?<Year>(?:\d{4}|\d{2}))(?x)"
    Private ZIP As String = "(?<Zip>\d{5})-(?<Sub>\d{4})(?x)"

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl1 overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents erpShowError As System.Windows.Forms.ErrorProvider
    Friend WithEvents erpShowRequired As System.Windows.Forms.ErrorProvider
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(ValidText))
        Me.erpShowError = New System.Windows.Forms.ErrorProvider()
        Me.erpShowRequired = New System.Windows.Forms.ErrorProvider()
        '
        'erpShowError
        '
        Me.erpShowError.Icon = CType(resources.GetObject("erpShowError.Icon"), System.Drawing.Icon)
        '
        'ValidText
        '

    End Sub

#End Region

#Region " Attributes "

#Region " Fields "

    Private m_ShowErrorIcon As Boolean
    Private m_ValidText As String
    Private m_MaskEdit As String
    Private m_FieldReference As String
    Private m_Required As Boolean
    Private m_ValidationMode As ValidationModes
    Private m_RegExPattern As RegularExpressionModes
    Private m_MessageLanguage As MessageLanguages

#End Region 'Fields

#Region " Properties "

    'COMENT: Sets error icon messages language
    <Description("Sets error icon messages language."), Category("Validation")> _
    Public Property MessageLanguage() As MessageLanguages
        Get
            Return m_MessageLanguage
        End Get
        Set(ByVal Value As MessageLanguages)
            m_MessageLanguage = Value
        End Set
    End Property 'ShowErrorIcon

    'COMENT: Message Languages supported
    Enum MessageLanguages
        English = 0
        Español = 1
    End Enum 'MessageLanguages

    'COMENT: Sets if error icon is showed.
    <Description("Sets if error icon is showed."), Category("Validation")> _
    Public Property ShowErrorIcon() As Boolean
        Get
            Return m_ShowErrorIcon
        End Get
        Set(ByVal Value As Boolean)
            m_ShowErrorIcon = Value
        End Set
    End Property 'ShowErrorIcon

    'COMENT: ValidText, invalidText or regularExpression to use in validation.
    <Description("ValidText, invalidText or regularExpression to use in validation."), Category("Validation")> _
    Public Property ValidText() As String
        Get
            Return m_ValidText
        End Get
        Set(ByVal Value As String)
            m_ValidText = Value
        End Set
    End Property 'ValidText

    'COMENT: Other characters are fixed in the mask.
    <Description("Mask. #:Numbers, A:Uppercase, a: lowercase, &&:Uppercase and lowercase, $:Uppercase, lowercase and Numbers" & _
    ". Other characters are fixed in the mask."), Category("Validation"), RefreshProperties(RefreshProperties.All)> _
    Public Property MaskEdit() As String
        Get
            Return m_MaskEdit
        End Get
        Set(ByVal Value As String)
            m_MaskEdit = Value
            If Not m_MaskEdit = "" Then
                Me.ValidationMode = ValidationModes.MaskEdit
            End If
        End Set
    End Property 'MaskEdit

    'COMENT: Field name to validate. Field name will be showed in the field required  error messages.
    <Description("Field name to validate. Field name will be showed in the field required  error messages."), Category("Validation")> _
    Public Property FieldReference() As String
        Get
            Return m_FieldReference
        End Get
        Set(ByVal Value As String)
            m_FieldReference = Value
        End Set
    End Property 'FieldReference

    'COMENT: Sets the field as required for validation.
    <Description("Sets the field as required for validation."), Category("Validation")> _
    Public Property Required() As Boolean
        Get
            Return m_Required
        End Get
        Set(ByVal Value As Boolean)
            m_Required = Value
            If m_Required = True Then
                Me.CausesValidation = True
            End If
        End Set
    End Property 'Required

    'COMENT: Validation Modes
    Enum ValidationModes
        None = 0
        ValidCharacters = 1
        InvalidCharacters = 2
        Letters = 3
        Numbers = 4
        MaskEdit = 5
        RegularExpression = 6
    End Enum 'ValidationMode

    'COMENT: Regular Expression Modes
    Enum RegularExpressionModes
        Custom = 0
        Email = 1
        Url = 2
        IP = 3
        Dates = 4
        Zip = 5
    End Enum 'RegularExpressionModes

    'COMENT: Sets regular expresssion Pattern to validate in RegularExpression validation mode.
    <Description("Sets regular expresssion Pattern to validate in RegularExpression validation mode."), Category("Validation")> _
    Public Property RegExPattern() As RegularExpressionModes
        Get
            Return m_RegExPattern
        End Get
        Set(ByVal Value As RegularExpressionModes)
            m_RegExPattern = Value
        End Set
    End Property 'RegExPattern

    'COMENT: Sets validation mode of ValidText control.
    <Description("Sets the validation mode to use."), Category("Validation"), RefreshProperties(RefreshProperties.All)> _
    Public Property ValidationMode() As ValidationModes
        Get
            Return m_ValidationMode
        End Get
        Set(ByVal Value As ValidationModes)
            m_ValidationMode = Value
            Select Case m_ValidationMode
                Case ValidationModes.None
                    '---------------------------
                    Me.m_ValidText = ""
                    Me.m_MaskEdit = ""
                    '---------------------------
                Case ValidationModes.ValidCharacters
                    '---------------------------
                    Me.m_MaskEdit = ""
                    '---------------------------
                Case ValidationModes.InvalidCharacters
                    '---------------------------
                    Me.m_MaskEdit = ""
                    '---------------------------
                Case ValidationModes.Letters
                    '---------------------------
                    Me.m_MaskEdit = ""
                    '---------------------------
                Case ValidationModes.Numbers
                    '---------------------------
                    Me.m_MaskEdit = ""
                    '---------------------------
                Case ValidationModes.MaskEdit
                    '---------------------------
                    If Me.ValidationMode = 5 Then
                        Dim i As Integer
                        Dim arrArray As String(,)
                        If Me.m_MaskEdit = "" Then
                            Me.m_MaskEdit = "Mask not set"
                        End If
                        maskString = ""
                        maskValues = ""
                        arrArray = getMask(Me.m_MaskEdit)
                        For i = 0 To CInt((((arrArray.Length) / 2) - 1))
                            maskString = maskString & arrArray(i, 0)
                            maskValues = maskValues & arrArray(i, 1)
                        Next i
                        Me.Text = maskString
                    End If
                    '---------------------------
            End Select
        End Set
    End Property 'ValidationMode

#End Region 'Properties

#End Region 'Atributos

#Region " Methods "

    'COMENT: Character entry validation.
    Private Sub eventTextChanged_validText(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.TextChanged
        If vexit = True Then
            vexit = False
            Exit Sub
        End If
        If secondLoad = True Then
            Me.lastText = Me.Text
            secondLoad = False
            thirdLoad = True
        End If
        If firstLoad = True Then
            Me.secondLoad = True
            firstLoad = False
        End If
        Me.erpShowRequired.SetError(Me, "")
        If Me.m_ValidationMode = ValidationModes.RegularExpression Then
            Me.erpShowError.SetError(Me, "")
        End If
        Dim i As Integer
        Dim j As Integer
        Dim check As Boolean = False
        Dim valid As Boolean = True
        Dim InvalidCharacters As New Collection()
        Select Case m_ValidationMode
            Case ValidationModes.ValidCharacters 'Caracteres valids
                '---------------------------
                If firstTime = True Then
                    Dim validString As String = Me.m_ValidText
                    check = False
                    If Not Me.Text = "" Then
                        firstTime = False
                        Dim lastCharacter As String = Mid(Me.Text, Me.Text.Length, 1)
                        For i = 0 To Me.Text.Length - 1
                            For j = 0 To validString.Length - 1
                                If Me.Text.Chars(i) = validString.Chars(j) Then
                                    check = True
                                End If
                            Next
                            If j = validString.Length And check = False Then
                                valid = False
                                InvalidCharacters.Add(Me.Text.Chars(i))
                            End If
                            check = False
                        Next
                        If valid = False Then
                            If Me.m_ShowErrorIcon = True Then
                                showError(InvalidCharacters)
                            End If
                            Me.Text = lastText
                            Me.SelectionStart = Me.Text.Length
                        Else
                            Me.erpShowError.SetError(Me, "")
                        End If
                        firstTime = True
                    Else
                        Me.erpShowError.SetError(Me, "")
                    End If
                    lastText = Me.Text
                End If
                '---------------------------

            Case ValidationModes.InvalidCharacters 'Caracteres invalids
                '---------------------------
                If firstTime = True Then
                    Dim validString As String = Me.m_ValidText
                    check = False
                    If Not Me.Text = "" Then
                        firstTime = False
                        Dim lastCharacter As String = Mid(Me.Text, Me.Text.Length, 1)
                        For i = 0 To Me.Text.Length - 1
                            For j = 0 To validString.Length - 1
                                If Me.Text.Chars(i) = validString.Chars(j) Then
                                    check = True
                                End If
                            Next
                            If j = validString.Length And check = True Then
                                valid = False
                                InvalidCharacters.Add(Me.Text.Chars(i))
                            End If
                            check = False
                        Next
                        If valid = False Then
                            If Me.m_ShowErrorIcon = True Then
                                showError(InvalidCharacters)
                            End If
                            Me.Text = lastText
                            Me.SelectionStart = Me.Text.Length
                        Else
                            Me.erpShowError.SetError(Me, "")
                        End If
                        firstTime = True
                    Else
                        Me.erpShowError.SetError(Me, "")
                    End If
                    lastText = Me.Text
                End If
                '---------------------------

            Case ValidationModes.Letters 'LETTERS
                '---------------------------
                If firstTime = True Then
                    Dim validString As String = "AÁBCDEÉFGHIÍJKLMNÑOÓPQRSTUÚÜVWXYZaábcdeéfghiíjklmnñoópqrstuúüvwxyz ,;.'"
                    check = False
                    If Not Me.Text = "" Then
                        firstTime = False
                        Dim lastCharacter As String = Mid(Me.Text, Me.Text.Length, 1)
                        For i = 0 To Me.Text.Length - 1
                            For j = 0 To validString.Length - 1
                                If Me.Text.Chars(i) = validString.Chars(j) Then
                                    check = True
                                End If
                            Next
                            If j = validString.Length And check = False Then
                                valid = False
                                InvalidCharacters.Add(Me.Text.Chars(i))
                            End If
                            check = False
                        Next
                        If valid = False Then
                            If Me.m_ShowErrorIcon = True Then
                                showError(InvalidCharacters)
                            End If
                            Me.Text = lastText
                            Me.SelectionStart = Me.Text.Length
                        Else
                            Me.erpShowError.SetError(Me, "")
                        End If
                        firstTime = True
                    Else
                        Me.erpShowError.SetError(Me, "")
                    End If
                    lastText = Me.Text
                End If
                '---------------------------

            Case ValidationModes.Numbers 'NUMBERS
                '---------------------------
                If firstTime = True Then
                    Dim validString As String = "1234567890.,"

                    'Dim r As New Regex("[1234567890.,]")
                    'Dim m As Match = r.Match("[^1234567890.,]")
                    'If Not Regex.IsMatch(Me.Text, r.ToString) Then
                    '    MsgBox(m.Captures())
                    'End If

                    check = False
                    If Not Me.Text = "" Then
                        firstTime = False
                        Dim lastCharacter As String = Mid(Me.Text, Me.Text.Length, 1)
                        For i = 0 To Me.Text.Length - 1
                            For j = 0 To validString.Length - 1
                                If Me.Text.Chars(i) = validString.Chars(j) Then
                                    check = True
                                End If
                            Next
                            If j = validString.Length And check = False Then
                                valid = False
                                InvalidCharacters.Add(Me.Text.Chars(i))
                            End If
                            check = False
                        Next
                        If valid = False Then
                            If Me.m_ShowErrorIcon = True Then
                                showError(InvalidCharacters)
                            End If
                            Me.Text = lastText
                            Me.SelectionStart = Me.Text.Length
                        Else
                            Me.erpShowError.SetError(Me, "")
                        End If
                        firstTime = True
                    Else
                        Me.erpShowError.SetError(Me, "")
                    End If
                    lastText = Me.Text
                End If
                '---------------------------

            Case ValidationModes.MaskEdit 'MaskEdit
                '---------------------------
                If maskBool = True Then
                    maskBool = False
                    Exit Sub
                End If
                If firstTime = True Then
                    Me.Text = maskString
                    maskText = maskString
                    firstTime = False
                Else
                    If Not Me.Text = "" Then
                        Dim cursorPosition As Integer
                        Dim charString As String
                        cursorPosition = Me.SelectionStart - 1
                        Select Case keyType
                            Case "BackSP"
                                Dim TemporalText As String = ""
                                Dim strMeTextTemp As String = ""
                                Dim counterTemp As Integer = 0
                                Dim counterLast As Integer = 0
                                Dim n As Integer
                                For i = 0 To Me.maskString.Length '- 1
                                    If i = cursorPosition + 1 Then
                                        For n = 0 To (Me.maskString.Length - Me.Text.Length) - 1
                                            strMeTextTemp &= "_"
                                            i += 1
                                        Next
                                    Else
                                        strMeTextTemp &= Me.Text.Chars(counterTemp)
                                        counterTemp += 1
                                    End If
                                Next
                                counterTemp = 0
                                For i = 0 To Me.maskString.Length - 1
                                    If strMeTextTemp.Chars(counterTemp) = Me.maskString.Chars(i) Then
                                        TemporalText &= Me.maskString.Chars(i)
                                        counterTemp += 1
                                        counterLast += 1
                                    Else
                                        If strMeTextTemp.Chars(counterTemp) = Me.lastText.Chars(counterLast) Then
                                            TemporalText &= Me.lastText.Chars(counterLast)
                                            counterTemp += 1
                                            counterLast += 1
                                        Else
                                            TemporalText &= Me.maskString.Chars(i)
                                            counterTemp += 1
                                            counterLast += 1
                                        End If
                                    End If
                                Next
                                vexit = True
                                Me.keyType = "Nothing"
                                Me.lastText = TemporalText
                                Me.maskText = TemporalText
                                Me.Text = TemporalText
                                Me.SelectionStart = cursorPosition + 1
                                Exit Sub

                            Case "Other"
                                If cursorPosition >= maskString.Length Then
                                    maskBool = True
                                    Me.Text = maskText
                                    Me.Select(cursorPosition + 1, 0)
                                Else
                                    charString = Me.Text.Chars(cursorPosition)
                                    While maskString.Chars(cursorPosition) <> "_" And cursorPosition < maskString.Length - 1
                                        cursorPosition = cursorPosition + 1
                                    End While
                                    If maskValues.Chars(cursorPosition) = "#" Or _
                                        maskValues.Chars(cursorPosition) = "A" Or _
                                        maskValues.Chars(cursorPosition) = "a" Or _
                                        maskValues.Chars(cursorPosition) = "&" Or _
                                        maskValues.Chars(cursorPosition) = "$" Then 'And _

                                        Select Case maskValues.Chars(cursorPosition)
                                            Case CChar("#")
                                                '-------------------------------
                                                For i = 0 To NUMBERS.Length - 1
                                                    If charString = NUMBERS.Chars(i) Then
                                                        validCharBool = True
                                                        Exit For
                                                    End If
                                                Next
                                                '-------------------------------
                                            Case CChar("A")
                                                '-------------------------------
                                                For i = 0 To LETTERS.Length - 1
                                                    If charString = LETTERS.Chars(i) Then
                                                        validCharBool = True
                                                        Exit For
                                                    End If
                                                Next
                                                '-------------------------------
                                            Case CChar("a")
                                                '-------------------------------
                                                Dim TemporalString As String
                                                For i = 0 To LETTERS.Length - 1
                                                    TemporalString = LETTERS.Chars(i)
                                                    If charString = TemporalString.ToLower Then
                                                        validCharBool = True
                                                        Exit For
                                                    End If
                                                Next
                                                '-------------------------------
                                            Case CChar("&")
                                                '-------------------------------
                                                Dim TemporalString As String
                                                For i = 0 To LETTERS.Length - 1
                                                    TemporalString = LETTERS.Chars(i)
                                                    If charString = TemporalString.ToLower _
                                                                    Or charString = LETTERS.Chars(i) Then
                                                        validCharBool = True
                                                        Exit For
                                                    End If
                                                Next
                                                '-------------------------------
                                            Case CChar("$")
                                                '-------------------------------
                                                Dim TemporalString As String
                                                For i = 0 To (LETTERS.Length - 1)
                                                    TemporalString = LETTERS.Chars(i)
                                                    If charString = TemporalString.ToLower _
                                                                    Or charString = LETTERS.Chars(i) Then
                                                        validCharBool = True
                                                        Exit For
                                                    End If
                                                Next
                                                If validCharBool = False Then
                                                    For i = 0 To NUMBERS.Length - 1
                                                        If charString = NUMBERS.Chars(i) Then
                                                            validCharBool = True
                                                            Exit For
                                                        End If
                                                    Next
                                                End If
                                                '-------------------------------
                                        End Select

                                        If validCharBool = True Then
                                            Dim strTempMask As String = ""
                                            For i = 0 To maskString.Length - 1
                                                If i = cursorPosition Then
                                                    strTempMask = strTempMask & charString
                                                Else
                                                    strTempMask = strTempMask & maskText.Chars(i)
                                                End If
                                            Next i
                                            maskText = strTempMask
                                            maskBool = True
                                            Me.Text = maskText
                                            Me.Select(cursorPosition + 1, 0)
                                            validCharBool = False
                                        Else
                                            maskBool = True
                                            Me.Text = maskText
                                            Me.Select(cursorPosition, 0)
                                        End If
                                    Else
                                        maskBool = True
                                        Me.Text = maskText
                                        Me.Select(cursorPosition + 1, 0)
                                    End If
                                End If
                            Case Else
                                Me.lastText = Me.Text
                                If keyType = "Nothing" Then
                                    maskText = Me.Text
                                    Exit Sub
                                End If
                                maskBool = True
                                Me.Text = maskText
                                If cursorPosition >= 0 Then
                                    Me.Select(cursorPosition + 1, 0)
                                Else
                                    Me.Select(0, 0)
                                End If
                        End Select
                        keyType = "Nothing"
                    End If
                End If
                Dim boolComp As Boolean = True
                For i = 0 To Me.Text.Length - 1
                    If Me.Text.Chars(i) = "_" Then
                        boolComp = False
                    End If
                Next
                If boolComp = True Then
                    Me.erpShowRequired.SetError(Me, "")
                End If

                If Me.Text = "" Then
                    ValidText_limpiaMaskEdit()
                End If
                '---------------------------
        End Select 'validacionEntrada
        If Not Me.thirdLoad = True Then
            Me.lastText = Me.Text
        Else
            Me.thirdLoad = False
        End If
        'MsgBox(lastText)
    End Sub

    'COMENT: Gets the pattern mask for MaskEdit validation mode.
    Private Function getMask(ByVal p_Pattern As String) As String(,)
        Dim i As Integer
        Dim arrMaskEdit(p_Pattern.Length - 1, 1) As String
        For i = 0 To p_Pattern.Length - 1
            If p_Pattern.Chars(i) = "#" Or p_Pattern.Chars(i) = "A" Or p_Pattern.Chars(i) = "a" _
                                    Or p_Pattern.Chars(i) = "&" Or p_Pattern.Chars(i) = "$" Then
                Select Case p_Pattern.Chars(i)
                    Case CChar("#")
                        '---------------------------
                        arrMaskEdit(i, 1) = "#"
                        '---------------------------
                    Case CChar("A")
                        '---------------------------
                        arrMaskEdit(i, 1) = "A"
                        '---------------------------
                    Case CChar("a")
                        '---------------------------
                        arrMaskEdit(i, 1) = "a"
                        '---------------------------
                    Case CChar("&")
                        '---------------------------
                        arrMaskEdit(i, 1) = "&"
                        '---------------------------
                    Case CChar("$")
                        '---------------------------
                        arrMaskEdit(i, 1) = "$"
                        '---------------------------
                End Select
                arrMaskEdit(i, 0) = "_"
            Else
                arrMaskEdit(i, 0) = p_Pattern.Chars(i)
                arrMaskEdit(i, 1) = p_Pattern.Chars(i)
            End If
        Next i
        Return arrMaskEdit

    End Function 'getMask

    'COMENT: Event. Sets key type when key in pressed.
    Public Sub eventKeyPress_validText(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Select Case e.KeyChar
            Case Microsoft.VisualBasic.ChrW(Keys.Back)
                keyType = "BackSP"
                'MsgBox(Me.Text)
            Case Else
                keyType = "Other"
        End Select
        If Me.m_ValidationMode = ValidationModes.MaskEdit Then
            If Me.Text = "" Then
                Me.Text = Me.m_MaskEdit
            End If
        End If
    End Sub 'eventKeyPress_validText

    'COMENT: Clear the mask.
    Public Sub ValidText_limpiaMaskEdit()
        If Me.m_ValidationMode = ValidationModes.MaskEdit Then
            While Me.Text <> maskString
                Me.Text = maskString
            End While
            Me.Select()
            Me.SelectionStart = 0
        End If
    End Sub 'ValidText_limpiaMaskEdit

    'COMENT: Lets to load data into MaskEdit validation.
    Public Sub ValidText_obtieneValor(ByVal p_strValor As String)
        If Me.m_ValidationMode = ValidationModes.MaskEdit Then
            While Me.Text <> p_strValor
                Me.Text = p_strValor
            End While
        End If
    End Sub 'ValidText_obtieneValor

    'COMENT: Event. Makes the validation when ValidText lost focus.
    Public Sub eventValidating_validText(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Validating
        If Me.m_Required = True And isRequired = 0 Then
            Select Case Me.m_ValidationMode
                Case Is = ValidationModes.MaskEdit
                    '---------------------------
                    Dim i As Integer
                    Dim bolMaskRequired As Boolean = False
                    If Not Me.Text = "" Then
                        For i = 0 To maskString.Length - 1
                            If Me.Text.Chars(i) = "_" Then
                                bolMaskRequired = True
                                Exit For
                            End If
                        Next
                    End If
                    If bolMaskRequired = True Then
                        e.Cancel = True
                        If Me.m_ShowErrorIcon = True Then
                            Me.erpShowError.SetError(Me, "")
                            Select Case Me.m_MessageLanguage
                                Case MessageLanguages.English
                                    Me.erpShowRequired.SetError(Me, "Field " & Me.m_FieldReference & " is required!")
                                Case MessageLanguages.Español
                                    Me.erpShowRequired.SetError(Me, "El campo " & Me.m_FieldReference & " es requerido!")
                            End Select

                        End If
                    End If
                    '---------------------------
                Case Else
                    '---------------------------
                    If Me.Text = "" Then
                        e.Cancel = True
                        If Me.m_ShowErrorIcon = True Then
                            Me.erpShowError.SetError(Me, "")
                            Select Case Me.m_MessageLanguage
                                Case MessageLanguages.English
                                    Me.erpShowRequired.SetError(Me, "Field " & Me.m_FieldReference & " is required!")
                                Case MessageLanguages.Español
                                    Me.erpShowRequired.SetError(Me, "El campo " & Me.m_FieldReference & " es requerido!")
                            End Select
                        End If
                    End If
                    '---------------------------
            End Select
        End If

    End Sub

    'COMENT: Event. event Validated_validText
    Public Sub eventValidated_validText(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Validated
        Me.erpShowRequired.SetError(Me, "")
    End Sub 'eventoValidate

    'COMENT: Event. Fires when ValidText gets focus..  
    Public Sub eventoEnter_validText(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Enter
        Dim i As Integer
        For i = 0 To Me.Text.Length - 1
            If Me.Text.Chars(i) = "_" Then
                Exit For
            End If
        Next
        Me.SelectionStart = i
    End Sub 'eventoEnter_validText

    'COMENT: Shows error icon.
    Private Sub showError(ByVal pInvalidCharacters As Collection)
        If pInvalidCharacters.Count > 1 Then
            Dim invalidCharactersList As String = ""
            Dim n As String
            For Each n In pInvalidCharacters
                invalidCharactersList &= "'" & n & "', "
            Next
            Select Case Me.m_MessageLanguage
                Case MessageLanguages.English
                    Me.erpShowError.SetError(Me, "Characters (" & invalidCharactersList.Trim.Remove(invalidCharactersList.Length - 2, 1) & ") are not allowed!")
                Case MessageLanguages.Español
                    Me.erpShowError.SetError(Me, "Los caracteres (" & invalidCharactersList.Trim.Remove(invalidCharactersList.Length - 2, 1) & ") no son permitidos!")
            End Select
        Else
            Select Case Me.m_MessageLanguage
                Case MessageLanguages.English
                    Me.erpShowError.SetError(Me, "Character ('" & CStr(pInvalidCharacters.Item(1)) & "') is not allowed!")
                Case MessageLanguages.Español
                    Me.erpShowError.SetError(Me, "El caracter ('" & CStr(pInvalidCharacters.Item(1)) & "') no es permitido!")
            End Select
        End If
    End Sub 'showError

    'COMENT: Muestra el ícono de Required en el control.
    Private Sub muestraRequired()
        Me.erpShowError.SetError(Me, "")
        Me.erpShowRequired.SetError(Me, "Field " & Me.m_FieldReference & " is Required!")
    End Sub 'muestraRequired

    'COMENT: Make all ValidText's required property False, to calcel its required validation.
    Public Shared Sub cancelrequiredFieldsCheck(ByVal pForm As Windows.Forms.Form)
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        Dim l As Integer
        For i = 0 To pForm.Controls.Count - 1
            If pForm.Controls(i).GetType.ToString = "ValidText.ValidText" Then
                CType(pForm.Controls(i), ValidText).isRequired = 1
                CType(pForm.Controls(i), ValidText).erpShowError.SetError(CType(pForm.Controls(i), ValidText), "")
                CType(pForm.Controls(i), ValidText).erpShowRequired.SetError(CType(pForm.Controls(i), ValidText), "")
            End If
            For j = 0 To pForm.Controls(i).Controls.Count - 1
                If pForm.Controls(i).Controls(j).GetType.ToString = "ValidText.ValidText" Then
                    CType(pForm.Controls(i).Controls(j), ValidText).isRequired = 1
                    CType(pForm.Controls(i).Controls(j), ValidText).erpShowError.SetError(CType(pForm.Controls(i).Controls(j), ValidText), "")
                    CType(pForm.Controls(i).Controls(j), ValidText).erpShowRequired.SetError(CType(pForm.Controls(i).Controls(j), ValidText), "")
                End If
                For k = 0 To pForm.Controls(i).Controls(j).Controls.Count - 1
                    If pForm.Controls(i).Controls(j).Controls(k).GetType.ToString = "ValidText.ValidText" Then
                        CType(pForm.Controls(i).Controls(j).Controls(k), ValidText).isRequired = 1
                        CType(pForm.Controls(i).Controls(j).Controls(k), ValidText).erpShowError.SetError(CType(pForm.Controls(i).Controls(j).Controls(k), ValidText), "")
                        CType(pForm.Controls(i).Controls(j).Controls(k), ValidText).erpShowRequired.SetError(CType(pForm.Controls(i).Controls(j).Controls(k), ValidText), "")
                    End If
                    For l = 0 To pForm.Controls(i).Controls(j).Controls(k).Controls.Count - 1
                        If pForm.Controls(i).Controls(j).Controls(k).Controls(l).GetType.ToString = "ValidText.ValidText" Then
                            CType(pForm.Controls(i).Controls(j).Controls(k).Controls(l), ValidText).isRequired = 1
                            CType(pForm.Controls(i).Controls(j).Controls(k).Controls(l), ValidText).erpShowError.SetError(CType(pForm.Controls(i).Controls(j).Controls(k).Controls(l), ValidText), "")
                            CType(pForm.Controls(i).Controls(j).Controls(k).Controls(l), ValidText).erpShowRequired.SetError(CType(pForm.Controls(i).Controls(j).Controls(k).Controls(l), ValidText), "")
                        End If
                    Next
                Next
            Next
        Next
    End Sub 'cancelrequiredFieldsCheck

    'COMENT: Return FALSE if an ValidText in form has text property = "" and required property = True
    '        In this case show the required icon for all valid text of this type.
    Public Shared Function requiredFieldsCheck(ByVal pForm As Windows.Forms.Form) As Boolean
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        Dim l As Integer
        Dim bolRequired As Boolean = False
        For i = 0 To pForm.Controls.Count - 1
            If pForm.Controls(i).GetType.ToString = "ValidText.ValidText" Then
                If CType(pForm.Controls(i), ValidText).Text = "" And CType(pForm.Controls(i), ValidText).Required = True Then
                    CType(pForm.Controls(i), ValidText).muestraRequired()
                    bolRequired = True
                End If
                If CType(pForm.Controls(i), ValidText).m_ValidationMode = ValidationModes.MaskEdit And CType(pForm.Controls(i), ValidText).Required = True Then
                    If Not CType(pForm.Controls(i), ValidText).Text = "" Then
                        Dim n As Integer
                        For n = 0 To CType(pForm.Controls(i), ValidText).Text.Length - 1
                            If CType(pForm.Controls(i), ValidText).Text.Chars(n) = "_" Then
                                CType(pForm.Controls(i), ValidText).muestraRequired()
                                bolRequired = True
                                Exit For
                            End If
                        Next
                    End If
                End If
            End If
            For j = 0 To pForm.Controls(i).Controls.Count - 1
                If pForm.Controls(i).Controls(j).GetType.ToString = "ValidText.ValidText" Then
                    If CType(pForm.Controls(i).Controls(j), ValidText).Text = "" And CType(pForm.Controls(i).Controls(j), ValidText).Required = True Then
                        CType(pForm.Controls(i).Controls(j), ValidText).muestraRequired()
                        bolRequired = True
                    End If
                    If CType(pForm.Controls(i).Controls(j), ValidText).m_ValidationMode = ValidationModes.MaskEdit And CType(pForm.Controls(i).Controls(j), ValidText).Required = True Then
                        If Not CType(pForm.Controls(i).Controls(j), ValidText).Text = "" Then
                            Dim n As Integer
                            For n = 0 To CType(pForm.Controls(i).Controls(j), ValidText).Text.Length - 1
                                If CType(pForm.Controls(i).Controls(j), ValidText).Text.Chars(n) = "_" Then
                                    CType(pForm.Controls(i).Controls(j), ValidText).muestraRequired()
                                    bolRequired = True
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                End If
                For k = 0 To pForm.Controls(i).Controls(j).Controls.Count - 1
                    If pForm.Controls(i).Controls(j).Controls(k).GetType.ToString = "ValidText.ValidText" Then
                        If CType(pForm.Controls(i).Controls(j).Controls(k), ValidText).Text = "" And CType(pForm.Controls(i).Controls(j).Controls(k), ValidText).Required = True Then
                            CType(pForm.Controls(i).Controls(j).Controls(k), ValidText).muestraRequired()
                            bolRequired = True
                        End If
                        If CType(pForm.Controls(i).Controls(j).Controls(k), ValidText).m_ValidationMode = ValidationModes.MaskEdit And CType(pForm.Controls(i).Controls(j).Controls(k), ValidText).Required = True Then
                            If Not CType(pForm.Controls(i).Controls(j).Controls(k), ValidText).Text = "" Then
                                Dim n As Integer
                                For n = 0 To CType(pForm.Controls(i).Controls(j).Controls(k), ValidText).Text.Length - 1
                                    If CType(pForm.Controls(i).Controls(j).Controls(k), ValidText).Text.Chars(n) = "_" Then
                                        CType(pForm.Controls(i).Controls(j).Controls(k), ValidText).muestraRequired()
                                        bolRequired = True
                                        Exit For
                                    End If
                                Next
                            End If
                        End If
                    End If
                    For l = 0 To pForm.Controls(i).Controls(j).Controls(k).Controls.Count - 1
                        If pForm.Controls(i).Controls(j).Controls(k).Controls(l).GetType.ToString = "ValidText.ValidText" Then
                            If CType(pForm.Controls(i).Controls(j).Controls(k).Controls(l), ValidText).Text = "" And CType(pForm.Controls(i).Controls(j).Controls(k).Controls(l), ValidText).Required = True Then
                                CType(pForm.Controls(i).Controls(j).Controls(k).Controls(l), ValidText).muestraRequired()
                                bolRequired = True
                            End If
                            If CType(pForm.Controls(i).Controls(j).Controls(k).Controls(l), ValidText).m_ValidationMode = ValidationModes.MaskEdit And CType(pForm.Controls(i).Controls(j).Controls(k).Controls(l), ValidText).Required = True Then
                                If Not CType(pForm.Controls(i).Controls(j).Controls(k).Controls(l), ValidText).Text = "" Then
                                    Dim n As Integer
                                    For n = 0 To CType(pForm.Controls(i).Controls(j).Controls(k).Controls(l), ValidText).Text.Length - 1
                                        If CType(pForm.Controls(i).Controls(j).Controls(k).Controls(l), ValidText).Text.Chars(n) = "_" Then
                                            CType(pForm.Controls(i).Controls(j).Controls(k).Controls(l), ValidText).muestraRequired()
                                            bolRequired = True
                                            Exit For
                                        End If
                                    Next
                                End If
                            End If
                        End If
                    Next
                Next
            Next
        Next
        If bolRequired = True Then
            Return False
        Else
            Return True
        End If
    End Function 'requiredFieldsCheck

    'COMENT: Evento. Here the regular expression is made.
    Private Sub eventoLeave_validText(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Leave
        If Not Me.Text = "" Then
            Dim strPatronExpresion As String
            Dim strTipo As String
            If Me.m_ValidationMode = ValidationModes.RegularExpression Then
                Select Case Me.m_RegExPattern
                    Case RegularExpressionModes.Custom  'Custom
                        '---------------------------
                        If Me.m_ValidText = "" Then
                            Exit Sub
                        End If
                        strPatronExpresion = Me.m_ValidText
                        '---------------------------

                    Case RegularExpressionModes.Email  'EMAIL
                        '---------------------------
                        strPatronExpresion = EMAIL
                        strTipo = "Email"
                        '---------------------------

                    Case RegularExpressionModes.Url 'URL
                        '---------------------------
                        strPatronExpresion = URL
                        strTipo = "URL"
                        '---------------------------

                    Case RegularExpressionModes.IP 'IP
                        '---------------------------
                        strPatronExpresion = IP
                        strTipo = "IP"
                        '---------------------------

                    Case RegularExpressionModes.Dates 'DATEC
                        '---------------------------
                        strPatronExpresion = DATEC
                        Select Case Me.m_MessageLanguage
                            Case MessageLanguages.English
                                strTipo = "Date"
                            Case MessageLanguages.Español
                                strTipo = "DFecha"
                        End Select
                        '---------------------------

                    Case RegularExpressionModes.Zip 'ZIP
                        '---------------------------
                        strPatronExpresion = ZIP
                        strTipo = "Zip code"
                        '---------------------------
                End Select
                If Not Regex.IsMatch(Me.Text, strPatronExpresion) Then
                    Select Case Me.m_MessageLanguage
                        Case MessageLanguages.English
                            If Me.m_RegExPattern = RegularExpressionModes.Custom Then
                                Me.erpShowError.SetError(Me, "The entered value does not has" & Chr(13) & "a valid format for the field.")
                            Else
                                Me.erpShowError.SetError(Me, "The entered value does not has" & Chr(13) & "a valid format for " & strTipo & ".")
                            End If
                        Case MessageLanguages.Español
                            If Me.m_RegExPattern = RegularExpressionModes.Custom Then
                                Me.erpShowError.SetError(Me, "El valor ingresado no posee" & Chr(13) & "un formato adecuado para este campo.")
                            Else
                                Me.erpShowError.SetError(Me, "El valor ingresado no posee" & Chr(13) & "un formato adecuado de " & strTipo & ".")
                            End If
                    End Select
                    Me.Select()
                    Me.SelectAll()
                Else
                    Me.erpShowError.SetError(Me, "")
                End If
            Else
                Me.erpShowError.SetError(Me, "")
            End If
        Else
            Me.erpShowError.SetError(Me, "")
        End If
    End Sub

    'COMENT: Restore the ValidText required property if it will change with cancelrequiredFieldsCheck method.
    '        This happen when ValidText's form load.
    Private Sub eventLoad_ValidTextForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miForm.Load
        If isRequired = 1 Then
            isRequired = 0
        End If
    End Sub

#End Region 'Methods

End Class
