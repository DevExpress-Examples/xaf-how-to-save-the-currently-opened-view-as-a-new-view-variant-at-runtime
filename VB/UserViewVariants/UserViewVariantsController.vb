Imports System
Imports System.Linq
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Utils
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Templates
Imports DevExpress.ExpressApp.Model.Core
Imports DevExpress.ExpressApp.SystemModule
Imports DevExpress.ExpressApp.ViewVariantsModule

Namespace UserViewVariants
    Public Class UserViewVariantsController
        Inherits ViewController

        Private Const STR_HasViewVariants_EnabledKey As String = "HasViewVariants"
        Private Const STR_IsRootViewVariant_EnabledKey As String = "IsRootViewVariant"
        Private Const STR_NewViewVariant_Id As String = "NewViewVariant"
        Private Const STR_DeleteViewVariant_Id As String = "DeleteViewVariant"
        Private Const STR_EditViewVariant_Id As String = "EditViewVariant"
        Private Const STR_UserViewVariants_Image As String = "Action_Copy"
        Private Const STR_NewViewVariant_Image As String = "Action_New"
        Private Const STR_DeleteViewVariant_Image As String = "Action_Delete"
        Private Const STR_EditViewVariant_Image As String = "Action_Edit"
        Private Const STR_UserViewVariants_Id As String = "UserViewVariants"

        Private ReadOnly userViewVariantsCore As SingleChoiceAction
        Protected changeVariantController As ChangeVariantController
        Protected rootModelViewVariants As IModelList(Of IModelVariant)
        Private modelViews As IModelList(Of IModelView)
        Private variantsProvider As IVariantsProvider
        Public Sub New()
            userViewVariantsCore = New SingleChoiceAction(Me, STR_UserViewVariants_Id, PredefinedCategory.View) With { _
                .ImageName = STR_UserViewVariants_Image, _
                .PaintStyle = ActionItemPaintStyle.CaptionAndImage, _
                .Caption = CaptionHelper.ConvertCompoundName(STR_UserViewVariants_Id), _
                .ItemType = SingleChoiceActionItemType.ItemIsOperation, _
                .ShowItemsOnClick = True _
            }
            Dim addViewVariantItem As New ChoiceActionItem(STR_NewViewVariant_Id, CaptionHelper.ConvertCompoundName(STR_NewViewVariant_Id), STR_NewViewVariant_Id) With {.ImageName = STR_NewViewVariant_Image}
            Dim removeViewVariantItem As New ChoiceActionItem(STR_DeleteViewVariant_Id, CaptionHelper.ConvertCompoundName(STR_DeleteViewVariant_Id), STR_DeleteViewVariant_Id) With {.ImageName = STR_DeleteViewVariant_Image}
            Dim editViewVariantItem As New ChoiceActionItem(STR_EditViewVariant_Id, CaptionHelper.ConvertCompoundName(STR_EditViewVariant_Id), STR_EditViewVariant_Id) With {.ImageName = STR_EditViewVariant_Image}
            userViewVariantsCore.Items.Add(addViewVariantItem)
            userViewVariantsCore.Items.Add(editViewVariantItem)
            userViewVariantsCore.Items.Add(removeViewVariantItem)
            AddHandler userViewVariantsCore.Execute, AddressOf UserViewVariants_Execute
        End Sub
        Private Sub UserViewVariants_Execute(ByVal sender As Object, ByVal e As SingleChoiceActionExecuteEventArgs)
            UserViewVariants(e)
        End Sub
        Protected Overridable Sub UserViewVariants(ByVal e As SingleChoiceActionExecuteEventArgs)
            Dim data As String = Convert.ToString(e.SelectedChoiceActionItem.Data)
            If data = STR_NewViewVariant_Id OrElse data = STR_EditViewVariant_Id Then
                ShowViewVariantParameterDialog(e, data)
            ElseIf data = STR_DeleteViewVariant_Id Then
                DeleteViewVariant()
            End If
        End Sub
        Protected Overrides Sub OnActivated()
            MyBase.OnActivated()
            Initialize()
            UpdateUserViewVariantsAction()
        End Sub
        Protected Overrides Sub Dispose(ByVal disposing As Boolean) ' It is important to release references here and not within the overridden OnDeactivated method, because this Controller is deactivated when changing the current View variant.
            If disposing Then
                UnsubscribeFromEvents()
                changeVariantController = Nothing
                variantsProvider = Nothing
                rootModelViewVariants = Nothing
                modelViews = Nothing
            End If
            MyBase.Dispose(disposing)
        End Sub
        Private Sub UnsubscribeFromEvents()
            RemoveHandler userViewVariantsCore.Execute, AddressOf UserViewVariants_Execute
            If (changeVariantController IsNot Nothing) AndAlso (changeVariantController.ChangeVariantAction IsNot Nothing) Then
                RemoveHandler changeVariantController.ChangeVariantAction.ExecuteCompleted, AddressOf ChangeVariantAction_ExecutedCompleted
            End If
        End Sub
        Private Sub Initialize()
            changeVariantController = Frame.GetController(Of ChangeVariantController)()
            If changeVariantController IsNot Nothing Then
                AddHandler changeVariantController.ChangeVariantAction.Executed, AddressOf ChangeVariantAction_ExecutedCompleted
            End If
            modelViews = DirectCast(View.Model.Application.Views, IModelList(Of IModelView))
            variantsProvider = Application.Modules.FindModule(Of ViewVariantsModule)().VariantsProvider
            rootModelViewVariants = DirectCast(DirectCast(modelViews(GetRootViewId()), IModelViewVariants).Variants, IModelList(Of IModelVariant))
        End Sub
        Private Sub ChangeVariantAction_ExecutedCompleted(ByVal sender As Object, ByVal e As ActionBaseEventArgs)
            UpdateUserViewVariantsAction()
        End Sub
        Private Function GetRootViewId() As String
            Dim variantsInfo As VariantsInfo = GetVariantsInfo()
            Return If(variantsInfo IsNot Nothing, variantsInfo.RootViewId, View.Id)
        End Function
        Protected Overridable Sub ShowViewVariantParameterDialog(ByVal e As SingleChoiceActionExecuteEventArgs, ByVal mode As String)
            Dim viewCaption As String = String.Empty
            Dim parameter As New ViewVariantParameterObject(rootModelViewVariants)
            If mode = STR_NewViewVariant_Id Then
                parameter.Caption = String.Format("{0}_{1:g}", View.Caption, Date.Now)
                viewCaption = CaptionHelper.GetLocalizedText("Texts", "NewViewVariantParameterCaption")
            End If
            If mode = STR_EditViewVariant_Id AndAlso (changeVariantController IsNot Nothing) AndAlso changeVariantController.ChangeVariantAction.SelectedItem IsNot Nothing Then
                parameter.Caption = changeVariantController.ChangeVariantAction.SelectedItem.Caption
                viewCaption = CaptionHelper.GetLocalizedText("Texts", "EditViewVariantParameterCaption")
            End If
            Dim dialogController As DialogController = Application.CreateController(Of DialogController)()
            AddHandler dialogController.Accepting, AddressOf dialogController_Accepting
            dialogController.Tag = mode
            Dim dv As DetailView = Application.CreateDetailView(ObjectSpaceInMemory.CreateNew(), parameter, False)
            dv.ViewEditMode = ViewEditMode.Edit
            dv.Caption = viewCaption
            e.ShowViewParameters.CreatedView = dv
            e.ShowViewParameters.Controllers.Add(dialogController)
            e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow
        End Sub
        Protected Sub dialogController_Accepting(ByVal sender As Object, ByVal e As DialogControllerAcceptingEventArgs)
            Dim dialogController As DialogController = DirectCast(sender, DialogController)
            RemoveHandler dialogController.Accepting, AddressOf dialogController_Accepting
            Dim data As String = Convert.ToString(dialogController.Tag)
            Dim parameter As ViewVariantParameterObject = TryCast(dialogController.Window.View.CurrentObject, ViewVariantParameterObject)
            If data = STR_NewViewVariant_Id Then
                NewViewVariant(parameter)
            ElseIf data = STR_EditViewVariant_Id Then
                EditViewVariant(parameter)
            End If
        End Sub
        Protected Overridable Sub NewViewVariant(ByVal parameter As ViewVariantParameterObject)
            View.SaveModel() 'It is necessary to save the current View settings into the application model before copying them.
            Dim newViewVariantId As String = String.Format("{0}_{1}", GetRootViewId(), Guid.NewGuid()) 'Identifier of a new view variant will be based on the identifier of the root view variant.
            Dim newModelViewVariant As IModelVariant = DirectCast(rootModelViewVariants, ModelNode).AddNode(Of IModelVariant)(newViewVariantId) ' Adds a new child node of the IModelVariant type with a specific identifier to the parent IModelViewVariants node.
            newModelViewVariant.View = TryCast((DirectCast(modelViews, ModelNode)).AddClonedNode(CType(View.Model, ModelNode), newViewVariantId), IModelView) ' Creates a new node of the IModelView type by cloning the settings of the current View and then sets the clone to the View property of the view variant created above. Beware of the specificity described at Q411979.
            newModelViewVariant.Caption = parameter.Caption 'Sets the Caption property of the view variant created above to the caption specified by an end-user in the dialog.
            If rootModelViewVariants.Count = 1 Then 'It is necessary to add a default view variant node for the current View for correct operation of the Change Variant Action.
                Dim currentModelViewVariant As IModelVariant = DirectCast(rootModelViewVariants, ModelNode).AddNode(Of IModelVariant)(View.Id)
                currentModelViewVariant.Caption = CaptionHelper.GetLocalizedText("Texts", "DefaultViewVariantCaption")
                currentModelViewVariant.View = View.Model
            End If
            If changeVariantController IsNot Nothing Then
                changeVariantController.CurrentFrameViewVariantsManager.RefreshVariants() 'Updates the Change Variant Action structure based on the model customizations above.
            End If
            UpdateCurrentViewVariant(newViewVariantId) 'Sets the current view variant to the newly created one.
            UpdateUserViewVariantsAction() 'Updates the items of our User View Variant Action based on the current the Change Variant Action structure.
        End Sub
        Protected Overridable Sub DeleteViewVariant() 'This method does almost the same work as NewViewVariant, but in reverse order.
            Dim variantsInfo As VariantsInfo = GetVariantsInfo() 'You should not be able to remove the root view variant.
            If variantsInfo IsNot Nothing AndAlso variantsInfo.CurrentVariantId <> GetRootViewId() Then
                UpdateCurrentViewVariant(String.Empty)
                rootModelViewVariants(variantsInfo.CurrentVariantId).Remove()
                modelViews(variantsInfo.CurrentVariantId).Remove()
                If changeVariantController IsNot Nothing Then
                    changeVariantController.CurrentFrameViewVariantsManager.RefreshVariants()
                End If
                UpdateUserViewVariantsAction()
            End If
            If rootModelViewVariants.Count = 1 Then
                rootModelViewVariants.ClearNodes()
            End If
        End Sub
        Protected Overridable Sub EditViewVariant(ByVal parameter As ViewVariantParameterObject)
            Dim variantsInfo As VariantsInfo = GetVariantsInfo()
            If variantsInfo IsNot Nothing Then
                rootModelViewVariants(variantsInfo.CurrentVariantId).Caption = parameter.Caption
            End If
            If changeVariantController IsNot Nothing Then
                changeVariantController.CurrentFrameViewVariantsManager.RefreshVariants()
            End If
        End Sub
        Protected Overridable Sub UpdateCurrentViewVariant(ByVal selectedViewId As String)
            If changeVariantController IsNot Nothing Then
                Dim action As SingleChoiceAction = changeVariantController.ChangeVariantAction
                Dim selectedItem As ChoiceActionItem = action.Items.FindItemByID(selectedViewId)
                If selectedItem IsNot Nothing Then
                    action.DoExecute(selectedItem)
                End If
            End If
        End Sub
        Private Sub UpdateUserViewVariantsAction()
            Dim hasViewVariants As Boolean = (GetVariantsInfo() IsNot Nothing)
            UserViewVarintsAction.Items.FindItemByID(STR_EditViewVariant_Id).Enabled(STR_HasViewVariants_EnabledKey) = hasViewVariants
            UserViewVarintsAction.Items.FindItemByID(STR_DeleteViewVariant_Id).Enabled(STR_HasViewVariants_EnabledKey) = UserViewVarintsAction.Items.FindItemByID(STR_EditViewVariant_Id).Enabled(STR_HasViewVariants_EnabledKey)

            If (changeVariantController IsNot Nothing) AndAlso changeVariantController.ChangeVariantAction.SelectedItem IsNot Nothing Then
                Dim variantInfo As VariantInfo = CType(changeVariantController.ChangeVariantAction.SelectedItem.Data, VariantInfo)
                UserViewVarintsAction.Items.FindItemByID(STR_DeleteViewVariant_Id).Enabled(STR_IsRootViewVariant_EnabledKey) = variantInfo.ViewID <> GetRootViewId()
            End If
        End Sub
        Private Function GetVariantsInfo() As VariantsInfo
            Return Application.Modules.FindModule(Of ViewVariantsModule)().FrameVariantsEngine.GetVariants(Frame.View)
        End Function
        Public ReadOnly Property UserViewVarintsAction() As SingleChoiceAction
            Get
                Return userViewVariantsCore
            End Get
        End Property
    End Class
End Namespace