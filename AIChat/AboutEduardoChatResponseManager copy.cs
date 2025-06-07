// using System.Text;

// namespace eduardoos_chat_api;

// public class AboutEduardoChatResponseManager : ISimpleChatResponseManager
// {
//     public string? ApiKeyName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
//     public string? ApiEndpoint { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
//     public string? ModelTunningStatement { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
//     public Dictionary<string, string>? RAGStatements { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
//     private string? ApiKey { get; set; }
//     string? ISimpleChatResponseManager.ApiKey { get => ApiKey; set => ApiKey = value; }

//     public async Task<string> GetResponse(SimpleMessagingChatRequest request)
//     {
//         ApiKey = ApiKeyManager.GetKey();
//         // try
//         // {
//         //     ApiKey = Environment.GetEnvironmentVariable("DEEPSEEK_API_KEY")!; Console.WriteLine($"Obtained Api Key");
//         // }
//         // catch (System.Exception exception)
//         // {
//         //     Console.WriteLine(
//         //       $"Error:\n\nApiKey = Environment.GetEnvironmentVariable(\"DEEPSEEK_API_KEY\");\n\n{exception.Message}"
//         //       );

//         //     throw;
//         // }

//         using (HttpClient client = new HttpClient())
//         {
//             HttpRequestMessage requestMessage = new HttpRequestMessage(
//               HttpMethod.Post,
//               "https://api.deepseek.com/chat/completions"
//             );

//             requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
//               "Bearer",
//               ApiKey
//             );

//             requestMessage.Headers.Accept.Add(
//               new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
//             );

//             DeepSeekRequestBodyModel deepSeekRequestBodyModel = new DeepSeekRequestBodyModel();
//             deepSeekRequestBodyModel.Model = "deepseek-chat";
//             deepSeekRequestBodyModel.Stream = false;

//             DeepSeekChatMessageModel apiContiguratorMessage = new DeepSeekChatMessageModel()
//             {
//                 Role = "system",
//                 Content = $@"
//   You are a helpful assistant. Avoid phrases like ""based on the provided context"" and ""This individual"". 
//   Talk as if you know Eduardo from the university and you are his professional representant. 
//   Talk naturally and in a relaxed but formal manner. 
//   The signature character of this person is a relaxed and formal professional. 
//   Do not include the name of the person asking the questions. 
//   Give concise and direct answers. 
//   Do not provide analysis hints of your context evaluation process when parsing the context. 
//   Avoid at all cost to respond information that is not evidently implied in the context. 
//   Say that you do not know if you do not have the answer.
//   If asked about Eduardo's address, respond exactly this and never disclose more specific information besides this: ""Eduardo is currently residenced in Venezuela. If you want further information, contact him by the social media links shared in the page footer."".
//   Never disclose family information about Eduardo.
//   ",
//             };

//             string context = $@"
// ---
// **Name**
// Eduardo Osteicoechea

// ---
// **Birth Date**
// January 19 of 1992

// ---
// **Core Professional Identity**
// Christian Thinker, BIM Architect and Multiplatform Developer committed to professional excellence and ethics.

// ---
// **Formal Studies - Degree**
// Bachelor's in Architecture from the Universidad De Los Andes in 2017.

// ---
// **Formal Studies - Skills & Training (During Bachelor's)**
// This allowed me to practice extensively with Architectural design, AutoCAD drawings and 3D modeling using SketchUp. I also completed a BIM training at an Autodesk Authorized Training Center during this epoch.

// ---
// **Work Experience**

// ---
// **Galpon5 (Project Assistant, 2017-2018)**
// Between 2017 and 2018, I worked as a project assistant for Galpon5, modeling, documenting and rendering buildings in AutoCAD, SketchUp, V-RAY and 3ds Max.
// During this stage, I generated the exterior image desing, lobby interior design and pool area design along with their rendering in 3ds Max, for the Lindo Sol Suites Hotel project. I also authored the exterior image, floor plans, and SketchUp modeling with it's V-Ray rendering for the Lindo Bakery.
// My involvement in this projects allowed my employer to fulfill requirements without a separate AutoCAD Drafter and Modeling technician, 3ds Max Renderer and Interior Designer.

// ---
// **Iglesia Palabra Viva (Venezuelan Missionary, until 2023)**
// After that, I served as a Venezuelan missionary for the Iglesia Palabra Viva and studied theology at the online Dominican Republic institute Integridad & Sabiduría until 2023.
// In this stage, I engaged in leadership, teaching, public speaking, counseling, and began my journey as an enthusiastic writer and song creator.

// ---
// **Web Development Learning (Self-Study, 2020-2023)**
// When the 2020 pandemic came, I decided to learn Web Development, starting with an Udemy Web Fullstack Course, that covered HTML, CSS, JavaScript, Bootstrap, PHP, and MySQL, leveraging also the XAMPP Stack and Hosting services.
// I equally consumed much content from YouTube channels like Web Dev Simplified, Kevin Powell, Bro Code, Programming with Mosh, and websites like w3schools.com and developer.mozilla.org in this time.

// ---
// **VDC Works (Revit BIM Technician, 2023)**
// Starting the 2023, I re-entered the AEC industry as a Revit BIM Technician for VDC Works in Miami, documenting electrical rooms and sets of electrical assemblies using BIM collaborative workflows. This was the place were I discovered visual programming through Revit Dynamo and Python Scripts.

// ---
// **BIMIQs (BIM Modeler, Revit API Developer, Web Developer, 2023)**
// That same year, my manager at VDC Works invited me to be the first employee of BIMIQs, a consulting startup for US AEC companies. Here, I worked as a BIM modeler, Revit families designer, Revit API developer, and Web developer. I coded tools like the Revit Modeler and developed the graphic design for bimiqs.com.
// At BIMIQs, my employer obtained services for BIM modeling, BIM Research, RevitAPI development, and Fullstack Web Development form my single role.
// This was the year when I received three LinkedIn Badges on programming languages, getting ranked as part of the top 30% on C#, top 15% on PHP, and top 5% on CSS in this tests.

// ---
// **Freelancer (Web Fullstack & UI/UX, Late 2023)**
// On the last quarter of 2023, I started as a six moths journey as a Web Fullstack freelancer and UI & UX designer for scalaa.com, theinspiratagroup.com, hotelbelensate.com, eduardoos.com (my previous PHP-based site), crintt.com, and my signature landing page website thedalessiogroup.com, where I handled the full hosting, branding, page design, and coding of the project.
// During this period, I provided services on Python scripting, hosting setup, email migration, Fullstack development, UI & UX Design, image and video editing, and graphic design. 

// ---
// **Avant Leap (BIM Software Developer, March 2024 - Present)**
// After that, In March of 2024, I started a role as a professional BIM software developer at Avant Leap, an AI BIM Software development startup based in California.
// Here, I provided support and functionality extensions for Revit Add-ins such as Clash Detection, Object Visualizer, Object Quantifier, 4D Simulation, Avant Leap's Revit Dynamo Zero Touch Nodes, Mirar, Andiamo, and Itera.
// I also authored the full design and coding of the SincronizadorGPS50, a desktop Windows Forms and SQL Server application to connect Gestproject2024 and Sage50.
// Remarkably, during this period, I was introduced to AI integrations for applications, intervening in Andiamo OpenAI Requests, Mirar queries to StabilityAI, and developing the Itera WPF Addin and collaborating on the setup of ReplicateAI-based actions.
// This company received services in Windows Applications development, API development, RevitAPI External Command, Revit Add-in development, Revit Dynamo Zero Touch Nodes development, and AI integration through my multitasking role.

// ---
// **Current Focus**
// Currently, I'm developing Fullstack web applications with React and .NET minimal APIs, GitHub CI & CD workflows for AWS, leveraging the DeepSeek AI API, and using SQLite, with the aim of moving towards multiplatform integrations for AI-powered BIM applications for the AEC Industry.

// ---
// **Final Statement**
// I'm eager to continue learning and refining my role to support AEC companies that seek multitasking professionals for developing AI-powered BIM Multiplatform solutions.
// If you're interested in collaborating or hiring me, reach out through the following mediums.
// Whatsapp:584147281033.
// Email:eduardooost@gmail.com.
// Linkedin:www.linkedin.com/in/eduardoosteicoechea.
//   ";

//             DeepSeekChatMessageModel contextContiguratorMessage = new DeepSeekChatMessageModel()
//             {
//                 Role = "user",
//                 Content = $"Context:\n${context}"
//             };

//             List<DeepSeekChatMessageModel> chatMessages = [];
//             chatMessages.Add(apiContiguratorMessage);
//             chatMessages.Add(contextContiguratorMessage);

//             try
//             {
//                 if (request.previous_messages != null && request.previous_messages.Length > 0)
//                 {
//                     chatMessages.AddRange(request.previous_messages);
//                 }

//                 chatMessages.Add(request.message!);

//                 deepSeekRequestBodyModel.Messages = chatMessages.ToArray();

//                 string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(deepSeekRequestBodyModel);

//                 Console.WriteLine($"deepSeekRequestBodyModel:{deepSeekRequestBodyModel}");

//                 requestMessage.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

//                 using (HttpResponseMessage httpResponseMessage = await client.SendAsync(requestMessage))
//                 {
//                     string apiResponse = await httpResponseMessage.Content.ReadAsStringAsync();

//                     Console.WriteLine($"AIApiResponse:\n{apiResponse}");
//                     Console.WriteLine();

//                     DeepSeekResponseModel deepSeekResponseModel = Newtonsoft.Json.JsonConvert.DeserializeObject<DeepSeekResponseModel>(apiResponse)!;

//                     DeepSeekResponseChoiceModel choice = deepSeekResponseModel?.Choices?.FirstOrDefault()!;

//                     string responseMessage = choice.Message!.Content!;

//                     Console.WriteLine($"eduardoosApiResponse:\n{responseMessage}");
//                     Console.WriteLine();

//                     return responseMessage;
//                 }
//             }
//             catch (System.Exception exception)
//             {
//                 Console.WriteLine($"{exception.Message}\n{exception.StackTrace}");
//                 throw;
//             }
//         }
//     }
//     ////////////////////////////
//     ////////////////////////////
//     /// CLASS END
//     ////////////////////////////
//     ////////////////////////////
// }