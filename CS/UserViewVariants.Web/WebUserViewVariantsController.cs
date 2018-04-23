using UserViewVariants;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web;

namespace UserViewVariants.Web {
    public class WebUserViewVariantsController : UserViewVariantsController {
        protected override void UpdateCurrentViewVariant(string selectedViewId) {
            base.UpdateCurrentViewVariant(selectedViewId);
            ((WebWindow)Application.MainWindow).RegisterStartupScript(Name, "window.location.reload();");
        }
        protected override void OnActivated() {
            base.OnActivated();
            UserViewVarintsAction.Active["ActiveInListViewOnly"] = View is ListView;
        }
    }
}