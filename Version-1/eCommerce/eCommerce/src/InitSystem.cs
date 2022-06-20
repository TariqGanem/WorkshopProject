using eCommerce.src.ServiceLayer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.src
{
    public class InitSystem
    {
        private static readonly string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        private static readonly Type FacadeType =
            Assembly.GetExecutingAssembly().GetTypes().First(t => t.Name == "eCommerceSystem");

        public static bool Test()
        {
            //CreateJson();
            return ReadStateFile(DesktopPath + @"\json\file.json");
            //Console.ReadKey();
        }

        //C:\Users\aeran\OneDrive\Desktop\json\file.json
        public static bool ReadStateFile(string path)
        {
            // wrap all this in try and different catches

            try
            {
                var jsonString = File.ReadAllText(path);
                var jsonArray = (JArray)JsonConvert.DeserializeObject(jsonString);

                if (jsonArray == null) return false; // check 1

                var tokenIndex = 0;

                foreach (var token in jsonArray)
                {
                    var json = (JObject)token;

                    if (json.Count != 2)
                        throw new Exception("Error : Illegal number of elements at json object num " + tokenIndex);

                    var methodName = (string)json["Method"];
                    var methodParams = json["Params"];

                    if (methodName == null || methodParams == null) throw new Exception("Wrong State File Format"); // check 2

                    var inst = eCommerceSystem.getInstance();
                    var method = FacadeType.GetMethod(methodName);

                    if (method == null) throw new Exception("Error : the method " + methodName + " is not supported");

                    if (method.GetParameters().Length != methodParams.ToArray().Length)
                        throw new Exception("Error : Illegal Params number for " + methodName);
                    var parameters = method.GetParameters()
                        .Select(p => Convert.ChangeType((string)methodParams[p.Position], p.ParameterType))
                        .ToArray();
                    var result = method.Invoke(inst, parameters); // check if null or false

                    switch (result)
                    {
                        case bool b when !b:
                            throw new Exception("Error : " + methodName + "Returned False!");
                        case null:
                            throw new Exception("Error : " + methodName + "Returned Null!");
                    }

                    tokenIndex++;
                }
            }
            catch (TargetInvocationException targetException)
            {
                // if (targetException.InnerException != null)
                //     throw targetException.InnerException;
                //  throw;
                return false;
            }
            catch (Exception e)
            {
                //throw new Exception(e.Message);
                return false;
            }

            return true;
        }

        private static void CreateJson()
        {
            var js = new List<FileData>
            {
                //* ----------------------------  Users -------------------------------*//*
                new FileData() {MethodName = "Register", MethodParams = new List<string> {"Eran", "passEran"}},
                new FileData() {MethodName = "Register", MethodParams = new List<string> {"Masalha", "passMasalha"}},
                new FileData() {MethodName = "Register", MethodParams = new List<string> {"Tareq", "passTareq"}},
                new FileData() {MethodName = "Register", MethodParams = new List<string> {"Yazan", "passYazan"}},
                new FileData() {MethodName = "Register", MethodParams = new List<string> {"Orabi", "passOrabi"}},
                new FileData() {MethodName = "Register", MethodParams = new List<string> {"Rami", "passRami"}},

                new FileData() {MethodName = "Login", MethodParams = new List<string> {"Eran", "passEran"}},
                new FileData() {MethodName = "Login", MethodParams = new List<string> {"Rami", "passRami"}},
                new FileData() {MethodName = "Login", MethodParams = new List<string> {"Orabi", "passOrabi"}},
                new FileData() {MethodName = "Login", MethodParams = new List<string> {"Tareq", "passTareq"}},


                //* ----------------------------- Stores ---------------------------------*//*

                
                new FileData() {MethodName = "OpenNewStoreUserName", MethodParams = new List<string> {"Eran's store", "Eran"}},
                new FileData() {MethodName = "AddStoreManagerInitFile", MethodParams = new List<string> { "Masalha", "Eran", "Eran's store"}},
                new FileData() {MethodName = "AddProductToStoreInitFile", MethodParams = new List<string> {"Eran", "Eran's store", "Bamba",3.ToString(), 100.ToString(),
                    "Snacks",null}},
                new FileData() {MethodName = "AddProductToStoreInitFile", MethodParams = new List<string> {"Eran", "Eran's store", "FunkoPop" , 40.ToString(), 10.ToString(),
                    "Anime",null}},
                new FileData() {MethodName = "AddProductToStoreInitFile", MethodParams = new List<string> {"Eran", "Eran's store", "BlackHoodie", 250.ToString(), 25.ToString(),
                    "Attire",null}},


                new FileData() {MethodName = "OpenNewStoreUserName", MethodParams = new List<string> {"Rami's store", "Rami"}},
                new FileData() {MethodName = "AddStoreManagerInitFile", MethodParams = new List<string> { "Yazan", "Rami", "Rami's store"}},
                new FileData() {MethodName = "AddStoreOwnerInitFile", MethodParams = new List<string> { "Tareq", "Rami", "Rami's store"}},
                new FileData() {MethodName = "AddProductToStoreInitFile", MethodParams = new List<string> {"Rami", "Rami's store", "Razer Mouse", 150.ToString(), 20.ToString(),
                    "PCGadjets",null}},
                new FileData() {MethodName = "AddProductToStoreInitFile", MethodParams = new List<string> {"Rami", "Rami's store", "Logitech Mouse", 175.ToString(), 15.ToString(),
                    "PCGadjets",null}},
                new FileData() {MethodName = "AddProductToStoreInitFile", MethodParams = new List<string> {"Rami", "Rami's store", "Samsung fridge lol", 1500.ToString(), 5.ToString(),
                    "Electronics",null}},


                new FileData() {MethodName = "AddProductToCartInitFile", MethodParams = new List<string> {"Orabi", "Bamba", "5", "Eran's store"}},
                new FileData() {MethodName = "AddProductToCartInitFile", MethodParams = new List<string> {"Orabi", "FunkoPop", "2", "Eran's store"}},
                new FileData() {MethodName = "AddProductToCartInitFile", MethodParams = new List<string> { "Tareq", "Razer Mouse", "1" ,"Rami's store"}},
                new FileData() {MethodName = "AddProductToCartInitFile", MethodParams = new List<string> { "Rami", "BlackHoodie", "2", "Eran's store"}},
                new FileData() {MethodName = "AddProductToCartInitFile", MethodParams = new List<string> { "Eran", "Samsung fridge lol", "1", "Rami's store"}},

                new FileData() {MethodName = "LogOutInitFile", MethodParams = new List<string> {"Tareq"}},
                new FileData() {MethodName = "LogOutInitFile", MethodParams = new List<string> {"Eran"}},
                new FileData() {MethodName = "LogOutInitFile", MethodParams = new List<string> {"Rami"}},
                new FileData() {MethodName = "LogOutInitFile", MethodParams = new List<string> {"Orabi"}},



};
            var json = JsonConvert.SerializeObject(js);
            try
            {
                File.WriteAllText(DesktopPath + @"\json\file.json", json);
            }
            catch
            {
                (new FileInfo(DesktopPath + @"\json\file.json")).Directory.Create();
                File.WriteAllText(DesktopPath + @"\json\file.json", json);
            }

        }

        [Serializable]
        private class FileData : ISerializable
        {
            protected internal string MethodName;
            protected internal List<string> MethodParams;

            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("Method", MethodName);
                info.AddValue("Params", MethodParams);
            }
        }
    }
}
