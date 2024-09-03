<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128592731/24.2.1%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E2813)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->

# XAF - How to save the currently open View as a new View Variant at runtime

This example shows how to create a controller that allows you to save the state of the current view as a view variant and apply saved variants to a view.

<kbd>![image](https://github.com/DevExpress-Examples/XAF_how-to-save-the-currently-opened-view-as-a-new-view-variant-at-runtime-e2813/assets/14300209/8a27e54e-167e-4d5d-a46c-f0dc6e5da901)</kbd>

> [!WARNING]
> We created this example for demonstration purposes and it is not intended to address all possible usage scenarios with it.
> If this example does not have certain functionality or you want to change its behavior, you can extend this example as needed. Please note that this can be a complex task that requires good knowledge of XAF: [UI Customization Categories by Skill Level](https://www.devexpress.com/products/net/application_framework/xaf-considerations-for-newcomers.xml#ui-customization-categories). You will likely need to research how our components work under the hood. Refer to the following help topic for more information: [Debug DevExpress .NET Source Code with PDB Symbols](https://docs.devexpress.com/GeneralInformation/403656/support-debug-troubleshooting/debug-controls-with-debug-symbols).
> We are unable to help with such tasks as custom programming is outside our Support Service scope: [Technical Support Scope](https://www.devexpress.com/products/net/application_framework/xaf-considerations-for-newcomers.xml#support).


## Implementation Details

To accomplish this task, we created a dedicated [controller](CS/EF/ViewVariantSaveEF/ViewVariantSaveEF.Module/Controllers/UserViewVariantsController.cs) that stores the state of a current view as a new view variant in an application model. The same controller allows users to apply existing view variants to a view.

## Files to Review

- [UserViewVariantsController.cs](CS/EF/ViewVariantSaveEF/ViewVariantSaveEF.Module/Controllers/UserViewVariantsController.cs) 
- [ViewVariantParameterObject.cs](CS/EF/ViewVariantSaveEF/ViewVariantSaveEF.Module/BusinessObjects/ViewVariantParameterObject.cs)

## Documentation 
- [View Variants (Switch Document Layouts)](https://docs.devexpress.com/eXpressAppFramework/113011/application-shell-and-base-infrastructure/view-variants-module)
- [Application Model (UI Settings Storage)](https://docs.devexpress.com/eXpressAppFramework/112579/ui-construction/application-model-ui-settings-storage)
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=xaf-how-to-save-the-currently-opened-view-as-a-new-view-variant-at-runtime&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=xaf-how-to-save-the-currently-opened-view-as-a-new-view-variant-at-runtime&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
