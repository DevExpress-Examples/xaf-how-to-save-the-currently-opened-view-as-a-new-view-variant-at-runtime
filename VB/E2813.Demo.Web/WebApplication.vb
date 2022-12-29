Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Xpo
Imports System
Imports System.ComponentModel
Imports DevExpress.ExpressApp.Web

Namespace E2813.Demo.Web

    Public Partial Class DemoAspNetApplication
        Inherits WebApplication

        Protected Overrides Sub CreateDefaultObjectSpaceProvider(ByVal args As CreateCustomObjectSpaceProviderEventArgs)
            args.ObjectSpaceProvider = New XPObjectSpaceProviderThreadSafe(args.ConnectionString, args.Connection)
        End Sub

        Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule

        Private module2 As DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule

        Private securityModule1 As DevExpress.ExpressApp.Security.SecurityModule

        Private securitySimple1 As DevExpress.ExpressApp.Security.SecuritySimple

        Private module6 As Objects.BusinessClassLibraryCustomizationModule

        Private authenticationActiveDirectory1 As DevExpress.ExpressApp.Security.AuthenticationActiveDirectory

        Private userViewVariantsWeb1 As UserViewVariants.Web.UserViewVariantsWeb

        Private viewVariantsModule1 As ViewVariantsModule.ViewVariantsModule

        Private validationModule1 As Validation.ValidationModule

        Private userViewVariantsModule1 As UserViewVariants.UserViewVariantsModule

        Private sqlConnection1 As Data.SqlClient.SqlConnection

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub DemoAspNetApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DatabaseVersionMismatchEventArgs)
#If EASYTEST
			e.Updater.Update();
			e.Handled = true;
#Else
            'if (System.Diagnostics.Debugger.IsAttached) {
            e.Updater.Update()
            e.Handled = True
        '}
        ' else {
        ' throw new InvalidOperationException(
        ' "The application cannot connect to the specified database, because the latter doesn't exist or its version is older than that of the application.\r\n" +
        ' "This error occurred  because the automatic database update was disabled when the application was started without debugging.\r\n" +
        ' "To avoid this error, you should either start the application under Visual Studio in debug mode, or modify the " +
        ' "source code of the 'DatabaseVersionMismatch' event handler to enable automatic database update, " +
        ' "or manually create a database using the 'DBUpdater' tool.\r\n" +
        ' "Anyway, refer to the 'Update Application and Database Versions' help topic at http://www.devexpress.com/Help/?document=ExpressApp/CustomDocument2795.htm " +
        ' "for more detailed information. If this doesn't help, please contact our Support Team at http://www.devexpress.com/Support/Center/");
        ' }
#End If
        End Sub

        Private Sub InitializeComponent()
            module1 = New SystemModule.SystemModule()
            module2 = New SystemModule.SystemAspNetModule()
            module6 = New Objects.BusinessClassLibraryCustomizationModule()
            securityModule1 = New Security.SecurityModule()
            securitySimple1 = New Security.SecuritySimple()
            authenticationActiveDirectory1 = New Security.AuthenticationActiveDirectory()
            sqlConnection1 = New Data.SqlClient.SqlConnection()
            userViewVariantsWeb1 = New UserViewVariants.Web.UserViewVariantsWeb()
            viewVariantsModule1 = New ViewVariantsModule.ViewVariantsModule()
            validationModule1 = New Validation.ValidationModule()
            userViewVariantsModule1 = New UserViewVariants.UserViewVariantsModule()
            CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
            ' 
            ' securitySimple1
            ' 
            securitySimple1.Authentication = authenticationActiveDirectory1
            securitySimple1.UserType = GetType(DevExpress.Persistent.BaseImpl.SimpleUser)
            ' 
            ' authenticationActiveDirectory1
            ' 
            authenticationActiveDirectory1.CreateUserAutomatically = True
            authenticationActiveDirectory1.LogonParametersType = Nothing
            ' 
            ' sqlConnection1
            ' 
            sqlConnection1.ConnectionString = "Data Source=(local);Initial Catalog=E2813.Demo;Integrated Security=SSPI;Pooling=f" & "alse"
            sqlConnection1.FireInfoMessageEventOnUserErrors = False
            ' 
            ' validationModule1
            ' 
            validationModule1.AllowValidationDetailsAccess = True
            validationModule1.IgnoreWarningAndInformationRules = False
            ' 
            ' DemoAspNetApplication
            ' 
            ApplicationName = "E2813"
            Connection = sqlConnection1
            Modules.Add(module1)
            Modules.Add(module2)
            Modules.Add(module6)
            Modules.Add(securityModule1)
            Modules.Add(viewVariantsModule1)
            Modules.Add(validationModule1)
            Modules.Add(userViewVariantsModule1)
            Modules.Add(userViewVariantsWeb1)
            Security = securitySimple1
            AddHandler DatabaseVersionMismatch, New EventHandler(Of DatabaseVersionMismatchEventArgs)(AddressOf DemoAspNetApplication_DatabaseVersionMismatch)
            CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        End Sub
    End Class
End Namespace
