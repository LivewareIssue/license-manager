<AttributeUsage(AttributeTargets.Class, AllowMultiple:=False, Inherited:=True)>
Public Class LicensedAddonAttribute
    Inherits Attribute
    Public ReadOnly Property Name As String

    Sub New(name As String)
        Me.Name = name
    End Sub
End Class