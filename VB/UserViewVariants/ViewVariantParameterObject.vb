Imports System.ComponentModel
Imports DevExpress.ExpressApp.DC
Imports DevExpress.ExpressApp.Model
Imports DevExpress.Persistent.Validation
Imports DevExpress.ExpressApp.ViewVariantsModule

Namespace UserViewVariants

    <DomainComponent>
    <DefaultProperty("Caption")>
    Public Class ViewVariantParameterObject

        Private ReadOnly variants As IModelList(Of IModelVariant)

        Public Sub New(ByVal variants As IModelList(Of IModelVariant))
            Me.variants = variants
        End Sub

        <RuleFromBoolProperty("RuleFromBoolProperty_ViewVariantParameterObject.IsUniqueCaption", "AddViewVariantContext", UsedProperties:="Caption", CustomMessageTemplate:="You must specify a different value, because there is already a view variant with the same caption.")>
        <Browsable(False)>
        Public ReadOnly Property IsUniqueCaption As Boolean
            Get
                Dim ok As Boolean = True
                For Each [variant] As IModelVariant In variants
                    If Equals([variant].Caption, Caption) Then
                        ok = False
                        Exit For
                    End If
                Next

                Return ok
            End Get
        End Property

        <RuleRequiredField("RuleRequiredField_ViewVariantParameterObject.Caption", "AddViewVariantContext")>
        Public Property Caption As String
    End Class
End Namespace
