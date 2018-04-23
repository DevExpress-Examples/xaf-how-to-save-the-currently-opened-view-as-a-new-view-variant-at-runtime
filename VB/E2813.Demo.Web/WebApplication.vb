Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Xpo
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports DevExpress.ExpressApp.Web

Namespace E2813.Demo.Web
    Partial Public Class DemoAspNetApplication
        Inherits WebApplication

        Protected Overrides Sub CreateDefaultObjectSpaceProvider(ByVal args As CreateCustomObjectSpaceProviderEventArgs)
            args.ObjectSpaceProvider = New XPObjectSpaceProviderThreadSafe(args.ConnectionString, args.Connection)
        End Sub
        Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule
        Private module2 As DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule
        Private securityModule1 As DevExpress.ExpressApp.Security.SecurityModule
        Private securitySimple1 As DevExpress.ExpressApp.Security.SecuritySimple
        Private module6 As DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule
        Private authenticationActiveDirectory1 As DevExpress.ExpressApp.Security.AuthenticationActiveDirectory
        Private userViewVariantsWeb1 As UserViewVariants.Web.UserViewVariantsWeb
        Private viewVariantsModule1 As DevExpress.ExpressApp.ViewVariantsModule.ViewVariantsModule
        Private validationModule1 As DevExpress.ExpressApp.Validation.ValidationModule
        Private userViewVariantsModule1 As UserViewVariants.UserViewVariantsModule
        Private sqlConnection1 As System.Data.SqlClient.SqlConnection

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub DemoAspNetApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs) Handles Me.DatabaseVersionMismatch
#If EASYTEST Then
            e.Updater.Update()
            e.Handled = True
#Else
            'if (System.Diagnostics.Debugger.IsAttached) {
                e.Updater.Update()
                e.Handled = True
            '}
'            else {
'                throw new InvalidOperationException(
'                    "The application cannot connect to the specified database, because the latter doesn't exist or its version is older than that of the application.\r\n" +
'                    "This error occurred  because the automatic database update was disabled when the application was started without debugging.\r\n" +
'                    "To avoid this error, you should either start the application under Visual Studio in debug mode, or modify the " +
'                    "source code of the 'DatabaseVersionMismatch' event handler to enable automatic database update, " +
'                    "or manually create a database using the 'DBUpdater' tool.\r\n" +
'                    "Anyway, refer to the 'Update Application and Database Versions' help topic at http://www.devexpress.com/Help/?document=ExpressApp/CustomDocument2795.htm " +
'                    "for more detailed information. If this doesn't help, please contact our Support Team at http://www.devexpress.com/Support/Center/");
'            }
#End If
        End Sub

        Private Sub InitializeComponent()
            Me.module1 = New DevExpress.ExpressApp.SystemModule.SystemModule()
            Me.module2 = New DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule()
            Me.module6 = New DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule()
            Me.securityModule1 = New DevExpress.ExpressApp.Security.SecurityModule()
            Me.securitySimple1 = New DevExpress.ExpressApp.Security.SecuritySimple()
            Me.authenticationActiveDirectory1 = New DevExpress.ExpressApp.Security.AuthenticationActiveDirectory()
            Me.sqlConnection1 = New System.Data.SqlClient.SqlConnection()
            Me.userViewVariantsWeb1 = New UserViewVariants.Web.UserViewVariantsWeb()
            Me.viewVariantsModule1 = New DevExpress.ExpressApp.ViewVariantsModule.ViewVariantsModule()
            Me.validationModule1 = New DevExpress.ExpressApp.Validation.ValidationModule()
            Me.userViewVariantsModule1 = New UserViewVariants.UserViewVariantsModule()
            DirectCast(Me, System.ComponentModel.ISupportInitialize).BeginInit()
            ' 
            ' securitySimple1
            ' 
            Me.securitySimple1.Authentication = Me.authenticationActiveDirectory1
            Me.securitySimple1.UserType = GetType(DevExpress.Persistent.BaseImpl.SimpleUser)
            ' 
            ' authenticationActiveDirectory1
            ' 
            Me.authenticationActiveDirectory1.CreateUserAutomatically = True
            Me.authenticationActiveDirectory1.LogonParametersType = Nothing
            ' 
            ' sqlConnection1
            ' 
            Me.sqlConnection1.ConnectionString = "Data Source=(local);Initial Catalog=E2813.Demo;Integrated Security=SSPI;Pooling=f" & "alse"
            Me.sqlConnection1.FireInfoMessageEventOnUserErrors = False
            ' 
            ' validationModule1
            ' 
            Me.validationModule1.AllowValidationDetailsAccess = True
            Me.validationModule1.IgnoreWarningAndInformationRules = False
            ' 
            ' DemoAspNetApplication
            ' 
            Me.ApplicationName = "E2813"
            Me.Connection = Me.sqlConnection1
            Me.Modules.Add(Me.module1)
            Me.Modules.Add(Me.module2)
            Me.Modules.Add(Me.module6)
            Me.Modules.Add(Me.securityModule1)
            Me.Modules.Add(Me.viewVariantsModule1)
            Me.Modules.Add(Me.validationModule1)
            Me.Modules.Add(Me.userViewVariantsModule1)
            Me.Modules.Add(Me.userViewVariantsWeb1)
            Me.Security = Me.securitySimple1
            DirectCast(Me, System.ComponentModel.ISupportInitialize).EndInit()

        End Sub
    End Class
End Namespace
