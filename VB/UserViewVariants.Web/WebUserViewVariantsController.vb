Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Web

Namespace UserViewVariants.Web

    Public Class WebUserViewVariantsController
        Inherits UserViewVariantsController

        Protected Overrides Sub UpdateCurrentViewVariant(ByVal selectedViewId As String)
            MyBase.UpdateCurrentViewVariant(selectedViewId)
            CType(Application.MainWindow, WebWindow).RegisterStartupScript(Name, "window.location.reload();")
        End Sub

        Protected Overrides Sub OnActivated()
            MyBase.OnActivated()
            UserViewVarintsAction.Active("ActiveInListViewOnly") = TypeOf View Is ListView
        End Sub
    End Class
End Namespace
