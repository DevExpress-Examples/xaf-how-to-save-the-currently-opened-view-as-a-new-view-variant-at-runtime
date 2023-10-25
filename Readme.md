<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128592731/23.1.6%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E2813)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# How to save the currently opened View as a new View Variant at runtime
This example shows how to create a conntroller that allow to save the current view's state as a view variant and apply saved variants to a view:
![image](https://github.com/DevExpress-Examples/XAF_how-to-save-the-currently-opened-view-as-a-new-view-variant-at-runtime-e2813/assets/14300209/8a27e54e-167e-4d5d-a46c-f0dc6e5da901)

## Implementation Details

To accomplish this task we created a dedicated [controller](CS/EF/ViewVariantSaveEF/ViewVariantSaveEF.Module/Controllers/UserViewVariantsController.cs) that stores a current view's state as a new view variant into an application's model. The same controller allows end-user to apply existing view variants to a view.

## Files to Review

- [UserViewVariantsController.cs](CS/EF/ViewVariantSaveEF/ViewVariantSaveEF.Module/Controllers/UserViewVariantsController.cs) 
- [ViewVariantParameterObject.cs](CS/EF/ViewVariantSaveEF/ViewVariantSaveEF.Module/BusinessObjects/ViewVariantParameterObject.cs)

## Documentation 
- [View Variants (Switch Document Layouts)](https://docs.devexpress.com/eXpressAppFramework/113011/application-shell-and-base-infrastructure/view-variants-module)
- [Application Model (UI Settings Storage)](https://docs.devexpress.com/eXpressAppFramework/112579/ui-construction/application-model-ui-settings-storage)
