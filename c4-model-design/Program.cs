using Structurizr;
using Structurizr.Api;

namespace c4_model_design
{
    class Program
    {
        static void Main(string[] args)
        {
            RenderModels();
        }

        static void RenderModels()
        {
            const long workspaceId = 77666;
            const string apiKey = "4373f1ba-1d75-4913-9351-6db16ff78070";
            const string apiSecret = "396439d2-208d-487d-a4ba-80858125325e";

            StructurizrClient structurizrClient = new StructurizrClient(apiKey, apiSecret);

            Workspace workspace = new Workspace("Favore", "Desarrolo de Applicaciones Moviles");

            ViewSet viewSet = workspace.Views;

            Model model = workspace.Model;

            // 1. Diagrama de Contexto
            SoftwareSystem favoreSystem = model.AddSoftwareSystem("Favore System", "Allows freelancers and clients to contact each other");
            SoftwareSystem Paypal = model.AddSoftwareSystem("PayPal Checkout System", "Allows users to make payments through Paypal third party application");
            SoftwareSystem GoogleFirebase = model.AddSoftwareSystem("Google Firebase System", "Allows users to register, log in, recover and change password and authenticate credentials");
            //SoftwareSystem MedicCenter = model.AddSoftwareSystem("Medic Center", "Plataforma que ofrece la información de los horarios y freelanceres de cada hospital o clínica a nuestro sistema.");
            //SoftwareSystem Apothecary = model.AddSoftwareSystem("Google Calender System", "Allows users to set reminders on their Google's account calendar");
            //SoftwareSystem Visa = model.AddSoftwareSystem("Google Calendar System", "Allows users to set reminders on their Google's account calendar");
            //SoftwareSystem Mastercard = model.AddSoftwareSystem("Zoom System", "Allows users to generates and participate in Zoom videoconferences");

            Person client = model.AddPerson("Client", "Client User");
            Person freelancer = model.AddPerson("Freelancer", "Freelancer User");
            //Person admin = model.AddPerson("Admin", "User Admin.");

            client.Uses(favoreSystem, "Interacts with");
            freelancer.Uses(favoreSystem, "Interacts with");


            //favoreSystem.Uses(MedicCenter, "Recibe información de los horarios y freelanceres de los diferentes centros médicos");
            //favoreSystem.Uses(Apothecary, "Uses Google Calendar event management service");

            favoreSystem.Uses(Paypal, "Use API to make payments");
            // favoreSystem.Uses(Visa, "Uses Google Calendar event management service");
            // favoreSystem.Uses(Mastercard, "Uses Zoom videocoference service");
            favoreSystem.Uses(GoogleFirebase, "Uses Google Firebase Authentication System");

            // Tags
            client.AddTags("client");
            freelancer.AddTags("freelancer");
            // admin.AddTags("Admin");

            favoreSystem.AddTags("FavoreSystem");
            //Apothecary.AddTags("Apothecary");
            //MedicCenter.AddTags("MedicCenter");
            Paypal.AddTags("Paypal");
            // Visa.AddTags("Visa");
            // Mastercard.AddTags("Mastercard");
            GoogleFirebase.AddTags("GoogleFirebase");

            Styles styles = viewSet.Configuration.Styles;
            styles.Add(new ElementStyle("client") { Background = "#0a60ff", Color = "#ffffff", Shape = Shape.Person });
            styles.Add(new ElementStyle("freelancer") { Background = "#aa20bf", Color = "#ffffff", Shape = Shape.Person });
            //styles.Add(new ElementStyle("Admin") { Background = "#aa70af", Color = "#ffffff", Shape = Shape.Person });
            styles.Add(new ElementStyle("FavoreSystem") { Background = "#008f39", Color = "#ffffff", Shape = Shape.RoundedBox });
            //styles.Add(new ElementStyle("MedicCenter") { Background = "#2f95c7", Color = "#ffffff", Shape = Shape.RoundedBox });
            //styles.Add(new ElementStyle("Apothecary") { Background = "#0a80ff", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("Paypal") { Background = "#60314c", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("Visa") { Background = "#FF8C00", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("Mastercard") { Background = "#8B0000", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("GoogleFirebase") { Background = "#4CAF50", Color = "#ffffff", Shape = Shape.RoundedBox });


            SystemContextView contextView = viewSet.CreateSystemContextView(favoreSystem, "Context", "Context Diagram");
            contextView.PaperSize = PaperSize.A4_Landscape;
            contextView.AddAllSoftwareSystems();
            contextView.AddAllPeople();

            // 2. Diagrama de Contenedores
            Container mobileApplication = favoreSystem.AddContainer("Mobile App", "Permite a los usuarios ver por un dashboard todo su registro de citas y prescripciones médicas.", "Swift UI");
            //Container webApplication = favoreSystem.AddContainer("Web Application", "Allows users to interact with PractiFinder Application", "PrimeVue UI");
            Container landingPage = favoreSystem.AddContainer("Landing Page", "", "React");
            Container apiRest = favoreSystem.AddContainer("API RESTful", "API Rest", "NodeJS");
            Container database = favoreSystem.AddContainer("Database", "", "");

            Container payment = favoreSystem.AddContainer("Payment Context", "Payment payment context", "");
            Container userContext = favoreSystem.AddContainer("User Profile Context", "User authentication and profile data");
            Container chatContext = favoreSystem.AddContainer("Chat Context", "Recruitmen management context", "");
            Container publicationContext = favoreSystem.AddContainer("Publication Context", "Evaluation management context", "");

            //Container logIn = favoreSystem.AddContainer("Log in", "Bounded Context de inicio de sesión");
            //Container notification = favoreSystem.AddContainer("Notifications", "Bounded Context de notificaciones");
            //Container userReviews = favoreSystem.AddContainer("User Review", "Bounded Context de las reseñas de los usuarios");
            //Container deliveryBounded = favoreSystem.AddContainer("DeliveryBounded", "Bounded Context de los deliverys ");
            //PRUEBA METHOS
            //Container PayMethods = favoreSystem.AddContainer("Pay Methods");


            client.Uses(mobileApplication, "Consult");
            //client.Uses(webApplication, "Consult");
            client.Uses(landingPage, "Consult");

            //admin.Uses(mobileApplication, "Consulta");
            // admin.Uses(webApplication, "Consult");
            // admin.Uses(landingPage, "Consult");

            freelancer.Uses(mobileApplication, "Consult");
            //freelancer.Uses(webApplication, "Consult");
            freelancer.Uses(landingPage, "Consult");

            mobileApplication.Uses(apiRest, "API Request", "JSON/HTTPS");
            //webApplication.Uses(apiRest, "API Request", "JSON/HTTPS");

            apiRest.Uses(payment, "", "");
            apiRest.Uses(publicationContext, "", "");
            apiRest.Uses(chatContext, "", "");
            apiRest.Uses(userContext, "", "");
            //apiRest.Uses(logIn, "", "");
            //apiRest.Uses(notification, "", "");
            //apiRest.Uses(userReviews, "", "");
            //apiRest.Uses(deliveryBounded, "", "");

            payment.Uses(database, "", "");
            publicationContext.Uses(database, "", "");
            chatContext.Uses(database, "", "");
            userContext.Uses(database, "", "");
            //logIn.Uses(database, "", "");
            //notification.Uses(database, "", "");
            //userReviews.Uses(database, "", "");
            //deliveryBounded.Uses(database, "", "");
            // payment.Uses(GoogleFirebase, "API Request", "JSON/HTTPS");
            //publicationContext.Uses(GoogleFirebase, "API Request", "JSON/HTTPS");
            chatContext.Uses(GoogleFirebase, "API Request", "JSON/HTTPS");
            //userContext.Uses(GoogleFirebase, "API Request", "JSON/HTTPS");

            //chatContext.Uses(Visa, "API Request", "JSON/HTTPS");
            //chatContext.Uses(Mastercard, "API Request", "JSON/HTTPS");

            //chatContext.Uses(Paypal, "API Request", "JSON/HTTPS");
            //chatContext.Uses(Visa, "API Request", "JSON/HTTPS");
            //chatContext.Uses(Mastercard, "API Request", "JSON/HTTPS");
            //chatContext.Uses(MedicCenter, "API Request", "JSON/HTTPS");
            //chatContext.Uses(PayMethods, "API Request", "JSON/HTTPS");

            //payment.Uses(Paypal, "API Request", "JSON/HTTPS");
            //payment.Uses(Visa, "API Request", "JSON/HTTPS");
            //payment.Uses(Mastercard, "API Request", "JSON/HTTPS");
            //payment.Uses(Apothecary, "API Request", "JSON/HTTPS");
            //payment.Uses(PayMethods, "API Request", "JSON/HTTPS");

            //deliveryBounded.Uses(Paypal, "API Request", "JSON/HTTPS");
            //deliveryBounded.Uses(Visa, "API Request", "JSON/HTTPS");
            //deliveryBounded.Uses(Mastercard, "API Request", "JSON/HTTPS");
            //deliveryBounded.Uses(GoogleFirebase, "API Request", "JSON/HTTPS");
            //deliveryBounded.Uses(PayMethods, "API Request", "JSON/HTTPS");

            //userContext.Uses(Apothecary, "API Request", "JSON/HTTPS");
            //publicationContext.Uses(Apothecary, "API Request", "JSON/HTTPS");

            //PRUEBA
            payment.Uses(Paypal, "API Request", "JSON/HTTPS");
            //payment.Uses(Visa, "API Request", "JSON/HTTPS");
            //payment.Uses(Mastercard, "API Request", "JSON/HTTPS");

            // Tags
            mobileApplication.AddTags("MobileApp");
            //webApplication.AddTags("WebApp");
            landingPage.AddTags("LandingPage");
            apiRest.AddTags("APIRest");
            database.AddTags("Database");

            payment.AddTags("payment");
            publicationContext.AddTags("publicationContext");
            chatContext.AddTags("Appointment");
            //logIn.AddTags("LogIn");
            //notification.AddTags("Notification");
            //userReviews.AddTags("Reviews");
            userContext.AddTags("userContext");
            //deliveryBounded.AddTags("Delivery");
            //PayMethods.AddTags("Payment Methods");

            string contextTag = "Context";

            payment.AddTags(contextTag);
            publicationContext.AddTags(contextTag);
            chatContext.AddTags(contextTag);
            //logIn.AddTags(contextTag);
            //notification.AddTags(contextTag);
            //userReviews.AddTags(contextTag);
            userContext.AddTags(contextTag);
            //deliveryBounded.AddTags(contextTag);

            // favoreSystem.AddContainer("Reviews of users", "Bounded Context de Reseñas de los usuarios", "NodeJS (NestJS)");
            // favoreSystem.AddContainer("Notifications", "Bounded Context de Notificaciones de los usuarios", "NodeJS (NestJS)");
            //styles.Add(new ElementStyle("WebApp") { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
            styles.Add(new ElementStyle("MobileApp") { Background = "#7d43d6", Color = "#ffffff", Shape = Shape.MobileDevicePortrait, Icon = "" });
            styles.Add(new ElementStyle("LandingPage") { Background = "#929000", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
            styles.Add(new ElementStyle("APIRest") { Shape = Shape.RoundedBox, Background = "#0000ff", Color = "#ffffff", Icon = "" });
            styles.Add(new ElementStyle("Database") { Shape = Shape.Cylinder, Background = "#ff0000", Color = "#ffffff", Icon = "" });
            styles.Add(new ElementStyle(contextTag) { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
            //styles.Add(new ElementStyle("Payment Methods") { Shape = Shape.RoundedBox, Background = "#220903", Color = "#ffffff", Icon = "" });

            ContainerView containerView = viewSet.CreateContainerView(favoreSystem, "Container", "Container Diagram");
            contextView.PaperSize = PaperSize.A4_Landscape;
            containerView.AddAllElements();

            // 3. Diagrama de Componentes (Monitoring Context)

            //appointment
            Component chatDomainLayer = chatContext.AddComponent("Chat Domain Layer", "", "");
            Component chatController = chatContext.AddComponent("Chat Controller", "", "REST API endpoints");
            //Component monitoringApplicationService = chatContext.AddComponent("Conference Controller", "", "REST API endpoints");
            //Component flightRepository = chatContext.AddComponent("Meeting Component", "", "");
            Component chatValidator = chatContext.AddComponent("Chat Validator", "", "REST API endpoints");
            Component chatHistoryRepository = chatContext.AddComponent("Chat History Repository", "", "");
            //Component ProductRepository = chatContext.AddComponent("ProductRepository", "Información de lote de vacunas", "NestJS Component");
            //Component publicationDomainLayerepository = chatContext.AddComponent("publicationDomainLayerepository", "Ubicación del vuelo", "NestJS Component");
            //Component aircraftSystemFacade = chatContext.AddComponent("Aircraft System Facade", "", "NestJS Component");

            //apiRest.Uses(monitoringApplicationService, "", "JSON/HTTPS");
            apiRest.Uses(chatController, "", "JSON/HTTPS");

            chatController.Uses(chatDomainLayer, "Uses");
            chatController.Uses(chatHistoryRepository, "Uses");
            chatController.Uses(chatValidator, "Uses");

            //monitoringApplicationService.Uses(flightRepository, "Uses");

            //monitoringApplicationService.Uses(chatDomainLayer, "Usa", "");
            //monitoringApplicationService.Uses(aircraftSystemFacade, "Usa");
            //monitoringApplicationService.Uses(flightRepository, "", "");
            //monitoringApplicationService.Uses(ProductRepository, "", "");
            //monitoringApplicationService.Uses(publicationDomainLayerepository, "", "");

            //flightRepository.Uses(database, "", "");
            chatHistoryRepository.Uses(database, "", "");
            chatHistoryRepository.Uses(GoogleFirebase, "Gets information about the chat", "[JSON/HTTPS]");
            chatValidator.Uses(database, "", "");
            chatValidator.Uses(GoogleFirebase, "Validates chat session", "[JSON/HTTPS]");

            //monitoringApplicationService.Uses(database, "", "");
            //flightRepository.Uses(GoogleFirebase, "JSON/HTTPS");
            //flightRepository.Uses(Mastercard, "JSON/HTTPS");
            //monitoringApplicationService.Uses(GoogleFirebase, "Validates chat session", "[JSON/HTTPS]");
            //ProductRepository.Uses(database, "", "");
            //publicationDomainLayerepository.Uses(database, "", "");
            //ProductRepository.Uses(Apothecary, "", "JSON/HTTPS");

            //publicationDomainLayerepository.Uses(googleMaps, "", "JSON/HTTPS");
            //publicationDomainLayerepository.Uses(Apothecary, "", "JSON/HTTPS");
            //publicationDomainLayerepository.Uses(MedicCenter, "", "JSON/HTTPS");


            // Tags
            chatValidator.AddTags("chatValidator");
            chatHistoryRepository.AddTags("chatHistoryRepository");
            chatDomainLayer.AddTags("chatDomainLayer");
            chatController.AddTags("chatController");
            //monitoringApplicationService.AddTags("MonitoringApplicationService");
            //flightRepository.AddTags("FlightRepository");
            //ProductRepository.AddTags("ProductRepository");
            //publicationDomainLayerepository.AddTags("publicationDomainLayerepository");
            //aircraftSystemFacade.AddTags("AircraftSystemFacade");

            styles.Add(new ElementStyle("chatDomainLayer") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("chatValidator") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("chatHistoryRepository") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("chatController") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("MonitoringApplicationService") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("MonitoringDomainModel") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("FlightStatus") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("FlightRepository") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            //styles.Add(new ElementStyle("ProductRepository") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            //styles.Add(new ElementStyle("publicationDomainLayerepository") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            //styles.Add(new ElementStyle("AircraftSystemFacade") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });

            ComponentView componentchatContext = viewSet.CreateComponentView(chatContext, "Components", "Components Chat");
            componentchatContext.PaperSize = PaperSize.A4_Landscape;
            componentchatContext.Add(mobileApplication);
            //componentchatContext.Add(webApplication);
            componentchatContext.Add(apiRest);
            componentchatContext.Add(database);
            componentchatContext.Add(GoogleFirebase);
            //  componentchatContext.Add(Mastercard);
            //componentchatContext.Add(googleMaps);
            // componentchatContext.Add(MedicCenter);
            componentchatContext.AddAllComponents();


            // 4. Diagrama de Componentes (payment)
            //payment
            //Component chatDomainLayer2 = payment.AddComponent("Domain Layer", "", "NodeJS (NestJS)");
            Component paymentController = payment.AddComponent("Payment Controller", "REST API endpoints", "REST controller");
            Component paymentService = payment.AddComponent("Payment Application Service", "Provides payment proccesing and validation methods", "");
            Component paymentFacade = payment.AddComponent("Payment Facade", "", "");
            //Component MedicamentRepository = payment.AddComponent("Medicament Repository", "Provee información del stock de los medicamentos", "NodeJS (NestJS) REST Controller");

            apiRest.Uses(paymentController, "", "JSON/HTTPS");
            paymentController.Uses(paymentService, "Invoques payment proccesing methods");
            //paymentService.Uses(chatDomainLayer2, "Usa");
            paymentService.Uses(paymentFacade, "Uses");
            //paymentService.Uses(MedicamentRepository, "");

            paymentFacade.Uses(Paypal, "JSON/HTTPS", "");
            //PayM.Uses(Visa, "JSON/HTTPS", "");
            //PayM.Uses(Mastercard, "JSON/HTTPS", "");
            //PayM.Uses(GoogleFirebase, "JSON/HTTPS", "");
            //MedicamentRepository.Uses(Apothecary, "", "JSON/HTTPS");

            //MedicamentRepository.Uses(database, "", "");
            paymentService.Uses(database, "", "");

            // Tags
            //chatDomainLayer2.AddTags("chatDomainLayer2");
            paymentController.AddTags("Controller");
            paymentService.AddTags("BuyApplicationService");
            paymentFacade.AddTags("MetodosPagos");
            //MedicamentRepository.AddTags("MedicamentRepository");

            styles.Add(new ElementStyle("chatDomainLayer2") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("Controller") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("BuyApplicationService") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("MetodosPagos") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            //styles.Add(new ElementStyle("MedicamentRepository") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });

            ComponentView componentadBuyMedicament = viewSet.CreateComponentView(payment, "Component", "Components Payments");
            componentadBuyMedicament.PaperSize = PaperSize.A4_Landscape;
            componentadBuyMedicament.Add(mobileApplication);
            //componentadBuyMedicament.Add(webApplication);
            componentadBuyMedicament.Add(apiRest);
            componentadBuyMedicament.Add(database);
            //componentadBuyMedicament.Add(GoogleFirebase);
            componentadBuyMedicament.Add(Paypal);
            //componentadBuyMedicament.Add(Visa);
            //componentadBuyMedicament.Add(Mastercard);
            componentadBuyMedicament.AddAllComponents();


            // 5. Diagrama de Componentes (userContext)
            //userContext
            Component profileComponent = userContext.AddComponent("User Profile Controller", "REST API endpoints", "");
            Component clientController = userContext.AddComponent("Client Controller", "REST API endpoints", "");
            Component freelancerController = userContext.AddComponent("Freelancer Controller", "REST API endpoints", "");
            Component featureComponent = userContext.AddComponent("Feature Component", "Allows users to use certain features", "");
            Component securityComponent = userContext.AddComponent("Security Component", "Provides functionality related to signing in, changing passwords, etc", "");
            Component notificationController = userContext.AddComponent("Notificaction Controller", "Provides functionality to send notification via email to user", "");
            Component notificationDomainLayer = userContext.AddComponent("Domain Layer", "", "");
            Component notificationComponent = userContext.AddComponent("Notification Component", "Sends and triggers  notification to user", "");


            apiRest.Uses(profileComponent, "", "JSON/HTTPS");
            apiRest.Uses(clientController, "", "JSON/HTTPS");
            apiRest.Uses(freelancerController, "", "JSON/HTTPS");
            apiRest.Uses(notificationController, "", "JSON/HTTPS");

            profileComponent.Uses(securityComponent, "Uses");
            securityComponent.Uses(GoogleFirebase, "JSON/HTTPS", "");
            securityComponent.Uses(database, "", "");

            clientController.Uses(featureComponent, "Uses");
            freelancerController.Uses(featureComponent, "Uses");
            featureComponent.Uses(database, "", "");

            notificationController.Uses(notificationDomainLayer, "Uses");
            notificationController.Uses(notificationComponent, "Uses");
            notificationComponent.Uses(GoogleFirebase, "JSON/HTTPS", "[Sends notification to email]");
            notificationComponent.Uses(database, "", "");

            //clientController.Uses(freelancerController, "Usa");
            //clientController.Uses(notificationController, "");
            //clientController.Uses(securityComponent, "");
            //securityComponent.Uses(Apothecary, "", "");

            // Tags
            notificationDomainLayer.AddTags("notificationDomainLayer");
            profileComponent.AddTags("profileComponent");
            clientController.AddTags("userContextApplicationService");
            freelancerController.AddTags("freelancerController");
            notificationController.AddTags("notificationController");
            securityComponent.AddTags("MedicamentRepositor");
            featureComponent.AddTags("featureComponentomp");
            notificationComponent.AddTags("NotifyComp");

            styles.Add(new ElementStyle("notificationDomainLayer") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("profileComponent") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("userContextApplicationService") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("freelancerController") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("notificationController") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("MedicamentRepositor") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("featureComponentomp") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("NotifyComp") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });


            ComponentView componentaduserContext = viewSet.CreateComponentView(userContext, "Componentss", "Component Users");
            componentaduserContext.PaperSize = PaperSize.A4_Landscape;
            componentaduserContext.Add(mobileApplication);
            //componentaduserContext.Add(webApplication);
            componentaduserContext.Add(apiRest);
            componentaduserContext.Add(database);
            componentaduserContext.Add(GoogleFirebase);
            componentaduserContext.AddAllComponents();

            // 6. Diagrama de Componentes (publication context)
            //
            Component publicationController = publicationContext.AddComponent("Publication Controller", "Provides evaluation revision and scoring", "REST API endpoints");
            Component publicationDomainLayer = publicationContext.AddComponent("Publication Domain Layer", "REST API endpoints", "");
            //Component ASServices = publicationContext.AddComponent("Evaluation Controller", "Provides evaluation creation and management", "REST API endpoints");
            Component serviceComponent = publicationContext.AddComponent("Service Component", "Answer for tasks", "");
            Component reviewComponent = publicationContext.AddComponent("Review Component", "Evaluation tasks", "");
            //Component securityComponent = publicationContext.AddComponent("Security Component", "Provides functionality related to signing in, changing passwords, etc", "");
            //Component featureComponent = publicationContext.AddComponent("Feature Component", "Allows users to use certain features", "");
            //Component notificationComponent = publicationContext.AddComponent("Notification Component", "Sends and triggers  notification to user", "");

            apiRest.Uses(publicationController, "", "JSON/HTTPS");

            // ASServices.Uses(publicationDomainLayer, "", "");
            //ASServices.Uses(reviewComponent, "Uses", "");

            publicationController.Uses(serviceComponent, "Uses", "");
            publicationController.Uses(reviewComponent, "Uses", "");
            publicationController.Uses(publicationDomainLayer, "Uses", "");

            serviceComponent.Uses(database, "", "");
            reviewComponent.Uses(database, "", "");

            //publicationController.Uses(ASServices, "Invoques room information processing methods");

            //ASServices.Uses(publicationDomainLayer, "Uses", "");
            //ASServices.Uses(serviceComponent, "Uses", "");

            //publicationDomainLayer.Uses(database, "", "");
            //serviceComponent.Uses(database, "", "");
            //publicationDomainLayer.Uses(GoogleFirebase, "JSON/HTTPS", "");
            //serviceComponent.Uses(GoogleFirebase, "JSON/HTTPS", "");

            //ASServices.Uses(reviewComponent, "Uses", "");
            //serviceComponent.Uses(Apothecary, "JSON/HTTPS", "Check publication date");

            // Tags
            reviewComponent.AddTags("reviewComponent");
            publicationController.AddTags("profileComponentT");
            //ASServices.AddTags("userContextApplicationServicee");
            publicationDomainLayer.AddTags("freelancerControllery");
            serviceComponent.AddTags("notificationControllery");
            //securityComponent.AddTags("MedicamentRepositor");
            //featureComponent.AddTags("featureComponentomp");
            //notificationComponent.AddTags("NotifyComp");

            styles.Add(new ElementStyle("reviewComponent") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("profileComponentT") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("userContextApplicationServicee") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("freelancerControllery") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("notificationControllery") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            //styles.Add(new ElementStyle("MedicamentRepositor") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            //styles.Add(new ElementStyle("featureComponentomp") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            //styles.Add(new ElementStyle("NotifyComp") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });


            ComponentView componentadASC = viewSet.CreateComponentView(publicationContext, "Componenss", "Component Accomodation Search");
            componentadASC.PaperSize = PaperSize.A4_Landscape;
            componentaduserContext.Add(mobileApplication);
            //componentadASC.Add(webApplication);
            componentadASC.Add(mobileApplication);
            componentadASC.Add(apiRest);
            componentadASC.Add(database);
            //componentadASC.Add(GoogleFirebase);
            //componentaduserContext.Add(Apothecary);
            componentadASC.AddAllComponents();

            structurizrClient.UnlockWorkspace(workspaceId);
            structurizrClient.PutWorkspace(workspaceId, workspace);
        }
    }
}