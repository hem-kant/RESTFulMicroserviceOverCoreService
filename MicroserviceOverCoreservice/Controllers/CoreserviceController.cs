using Coreservice.Client.CoreService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Xml;
using System.Xml.Linq;
using Tridion.ContentManager.CoreService.Client;

namespace MicroserviceOverCoreservice.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CoreserviceController : ApiController
    {
        public static ICoreServiceFrameworkContext coreService = null;
        public string getPublications()
        {
            return "pubids";
        }
        #region getComponentByTcmID
        public string getComponentByTcm(string tcmuri)
        {
            try
            {
                coreService = CoreServiceFactory.GetCoreServiceContext(new Uri(ConfigurationManager.AppSettings["CoreServiceURL"].ToString()), new NetworkCredential(ConfigurationManager.AppSettings["UserName"].ToString(), ConfigurationManager.AppSettings["Password"].ToString(), ConfigurationManager.AppSettings["Domain"].ToString()));               
                var component = coreService.Client.Read("tcm:" + tcmuri, null) as ComponentData;
                return component.Content.ToString();
            }


            catch (Exception ex)
            {
                
                return ex.Message.ToString();
            }

        }
        #endregion

        #region getSchemaFieldsByTcmID
        public string getSchemaByTcm(string tcmuri)
        {
            string xmljson = "XML";
            try
            {
                string output = string.Empty;
                coreService = CoreServiceFactory.GetCoreServiceContext(new Uri(ConfigurationManager.AppSettings["CoreServiceURL"].ToString()), new NetworkCredential(ConfigurationManager.AppSettings["UserName"].ToString(), ConfigurationManager.AppSettings["Password"].ToString(), ConfigurationManager.AppSettings["Domain"].ToString()));
                
                SchemaFieldsData schemaFieldsData = coreService.Client.ReadSchemaFields("tcm:" + tcmuri, false, null);
                if (xmljson.ToString().ToLower() == "json")
                {
                    output = JsonConvert.SerializeObject(schemaFieldsData);
                }
                else
                {
                    output = JsonConvert.SerializeObject(schemaFieldsData);
                    XmlDocument doc = new XmlDocument();

                    using (var reader = JsonReaderWriterFactory.CreateJsonReader(Encoding.UTF8.GetBytes(output), XmlDictionaryReaderQuotas.Max))
                    {
                        XElement xml = XElement.Load(reader);
                        doc.LoadXml(xml.ToString());
                    }
                    output = doc.InnerXml.ToString();
                }
                return output;// xmljson.ToString().ToLower() == "json" ? ConvertTojson.ConvertXmlToJson(schemasXml.ToString()) : schemasXml.ToString(); 

            }
            catch (Exception ex)
            {
                
                return ex.Message.ToString();
            }

        }
        #endregion

        #region GetAllCategoriesWithInPubByPubID
        public string GetAllCategoriesWithInPubByTcmUri(string tcmuri)
        {
            string xmljson = "xml";
            try
            {
                string output = string.Empty;
                coreService = CoreServiceFactory.GetCoreServiceContext(new Uri(ConfigurationManager.AppSettings["CoreServiceURL"].ToString()), new NetworkCredential(ConfigurationManager.AppSettings["UserName"].ToString(), ConfigurationManager.AppSettings["Password"].ToString(), ConfigurationManager.AppSettings["Domain"].ToString()));
                
                var componentData = coreService.Client.GetList("tcm:" + tcmuri, new CategoriesFilterData()); //TridionComponent.GetAllCategoriesWithInPubByTcmUri(coreService, tcmuri);
                if (xmljson.ToString().ToLower() == "json")
                {
                    output = JsonConvert.SerializeObject(componentData);
                }
                else
                {
                    output = JsonConvert.SerializeObject(componentData);
                    XmlDocument doc = new XmlDocument();

                    using (var reader = JsonReaderWriterFactory.CreateJsonReader(Encoding.UTF8.GetBytes(output), XmlDictionaryReaderQuotas.Max))
                    {
                        XElement xml = XElement.Load(reader);
                        doc.LoadXml(xml.ToString());
                    }
                    output = doc.InnerXml.ToString();
                }
                return output;
            }


            catch (Exception ex)
            {                
                return ex.Message.ToString();
            }

        }
        #endregion

        #region GetKeywordByCategory
        public string GetKeywordByCategory(string tcmuri)
        {
            string xmljson = "xml";
            try
            {
                coreService = CoreServiceFactory.GetCoreServiceContext(new Uri(ConfigurationManager.AppSettings["CoreServiceURL"].ToString()), new NetworkCredential(ConfigurationManager.AppSettings["UserName"].ToString(), ConfigurationManager.AppSettings["Password"].ToString(), ConfigurationManager.AppSettings["Domain"].ToString()));                
                var filter = new OrganizationalItemItemsFilterData();
                var category = "tcm:" + tcmuri;
                var keywords = coreService.Client.GetListXml(category, filter);

                return xmljson.ToString().ToLower() == "json" ? JsonConvert.SerializeObject(keywords) : keywords.ToString();

            }
            catch (Exception ex)
            {                
                return ex.Message.ToString();
            }

        }
        #endregion

        #region GetPageTempletByPubID
        public string GetPageTempletByPubID(string tcmuri)
        {
            string xmljson = "xml";
            try
            {
                coreService = CoreServiceFactory.GetCoreServiceContext(new Uri(ConfigurationManager.AppSettings["CoreServiceURL"].ToString()), new NetworkCredential(ConfigurationManager.AppSettings["UserName"].ToString(), ConfigurationManager.AppSettings["Password"].ToString(), ConfigurationManager.AppSettings["Domain"].ToString()));

                
                var filter = new RepositoryItemsFilterData();
                filter.ItemTypes = new[] { ItemType.PageTemplate, };
                filter.Recursive = true;
                var listXml = coreService.Client.GetListXml("tcm:" + tcmuri, filter);
                //string output = JsonConvert.SerializeObject(listXml);
                return xmljson.ToLower() == "json" ? JsonConvert.SerializeObject(listXml) : listXml.ToString();
            }
            catch (Exception ex)
            {
               
                return ex.Message.ToString();
            }

        }
        #endregion

        #region GetComponentTemplateByPubID
        public string GetComponentTemplateByPubID(string tcmuri)
        {
            string xmljson = "xml";
            try
            {
                coreService = CoreServiceFactory.GetCoreServiceContext(new Uri(ConfigurationManager.AppSettings["CoreServiceURL"].ToString()), new NetworkCredential(ConfigurationManager.AppSettings["UserName"].ToString(), ConfigurationManager.AppSettings["Password"].ToString(), ConfigurationManager.AppSettings["Domain"].ToString()));
                
                var filter = new RepositoryItemsFilterData();
                filter.ItemTypes = new[] { ItemType.ComponentTemplate, };
                filter.Recursive = true;
                var listXml = coreService.Client.GetListXml("tcm:" + tcmuri, filter);
                return xmljson.ToLower() == "json" ? JsonConvert.SerializeObject(listXml) : listXml.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

        }
        #endregion

        #region GetTemplateBuildingBlockByPubID
        public string GetTemplateBuildingBlockByPubID(string tcmuri)
        {
            string xmlOrJson = "xml";
            try
            {
                coreService = CoreServiceFactory.GetCoreServiceContext(new Uri(ConfigurationManager.AppSettings["CoreServiceURL"].ToString()), new NetworkCredential(ConfigurationManager.AppSettings["UserName"].ToString(), ConfigurationManager.AppSettings["Password"].ToString(), ConfigurationManager.AppSettings["Domain"].ToString()));
                string output = string.Empty;
                var filter = new RepositoryItemsFilterData();
                filter.ItemTypes = new[] { ItemType.TemplateBuildingBlock, };
                filter.Recursive = true;
                var listXml = coreService.Client.GetListXml("tcm:"+tcmuri, filter);
                if (xmlOrJson.ToString().ToLower() == "json")
                {
                    output = JsonConvert.SerializeObject(listXml);
                }
                else
                {
                    output = listXml.ToString();
                }

                return output;
            }


            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

        }
        #endregion

        #region GetPageByPubID
        public string GetPageByPubID(string tcmuri)
        {
            string xmlOrJson = "xml";
            try
            {
                coreService = CoreServiceFactory.GetCoreServiceContext(new Uri(ConfigurationManager.AppSettings["CoreServiceURL"].ToString()), new NetworkCredential(ConfigurationManager.AppSettings["UserName"].ToString(), ConfigurationManager.AppSettings["Password"].ToString(), ConfigurationManager.AppSettings["Domain"].ToString()));
                string output = string.Empty;
                var filter = new RepositoryItemsFilterData();
                filter.ItemTypes = new[] { ItemType.Page, };
                filter.Recursive = true;
                var listXml = coreService.Client.GetListXml("tcm:" + tcmuri, filter);
                if (xmlOrJson.ToString().ToLower() == "json")
                {
                    output = JsonConvert.SerializeObject(listXml);
                }
                else
                {
                    output = listXml.ToString();
                }

                return output;
            }


            catch (Exception ex)
            {               
                return ex.Message.ToString();
            }

        }
        #endregion

        #region GetStructureGroupByPubID
        public string GetStructureGroupByPubID(string tcmuri)
        {
            string xmlOrJson = "xml";
            try
            {
                coreService = CoreServiceFactory.GetCoreServiceContext(new Uri(ConfigurationManager.AppSettings["CoreServiceURL"].ToString()), new NetworkCredential(ConfigurationManager.AppSettings["UserName"].ToString(), ConfigurationManager.AppSettings["Password"].ToString(), ConfigurationManager.AppSettings["Domain"].ToString()));
                string output = string.Empty;
                var filter = new RepositoryItemsFilterData();
                filter.ItemTypes = new[] { ItemType.StructureGroup, };
                filter.Recursive = true;
                var listXml = coreService.Client.GetListXml("tcm:"+tcmuri, filter);
                if (xmlOrJson.ToString().ToLower() == "json")
                {
                    output = JsonConvert.SerializeObject(listXml);
                }
                else
                {
                    output = listXml.ToString();
                }

                return output;
            }


            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

        }
        #endregion

        #region GetMultimediaComponentByPubID
        public string GetMultimediaComponentByPubID(string tcmuri)
        {
           string xmlOrJson = "xml";
            try
            {
                coreService = CoreServiceFactory.GetCoreServiceContext(new Uri(ConfigurationManager.AppSettings["CoreServiceURL"].ToString()), new NetworkCredential(ConfigurationManager.AppSettings["UserName"].ToString(), ConfigurationManager.AppSettings["Password"].ToString(), ConfigurationManager.AppSettings["Domain"].ToString()));
                string output = string.Empty;
               
                var filter = new RepositoryItemsFilterData();
                filter.ItemTypes = new ItemType[] { ItemType.Component };
                filter.ComponentTypes = new ComponentType[] { ComponentType.Multimedia };

                filter.Recursive = true;
                var listXml = coreService.Client.GetListXml("tcm:" + tcmuri, filter);
                if (xmlOrJson.ToString().ToLower() == "json")
                {
                    output = JsonConvert.SerializeObject(listXml);
                }
                else
                {
                    output = listXml.ToString();
                }

                return output;
            }


            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

        }
        #endregion

        #region GetPublicationList
        public string GetPublicationList()
        {
            string xmlOrJson = "xml";
            try
            {
                coreService = CoreServiceFactory.GetCoreServiceContext(new Uri(ConfigurationManager.AppSettings["CoreServiceURL"].ToString()), new NetworkCredential(ConfigurationManager.AppSettings["UserName"].ToString(), ConfigurationManager.AppSettings["Password"].ToString(), ConfigurationManager.AppSettings["Domain"].ToString()));
                string output = string.Empty;
                XmlDocument publicationList = new XmlDocument();
                PublicationsFilterData filter = new PublicationsFilterData();
                XElement publications = coreService.Client.GetSystemWideListXml(filter);
                publicationList.Load(publications.CreateReader());
                if (xmlOrJson.ToString().ToLower() == "json")
                {
                    output = JsonConvert.SerializeObject(publicationList);
                }
                else
                {
                    output = publicationList.InnerXml.ToString();
                }
                return output;
            }


            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

        }
        #endregion

        #region GetUserList
        public string GetUserList()
        {
            string xmlOrJson = "xml";
            try
            {
                coreService = CoreServiceFactory.GetCoreServiceContext(new Uri(ConfigurationManager.AppSettings["CoreServiceURL"].ToString()), new NetworkCredential(ConfigurationManager.AppSettings["UserName"].ToString(), ConfigurationManager.AppSettings["Password"].ToString(), ConfigurationManager.AppSettings["Domain"].ToString()));
                string output = string.Empty;                
                var listXml = coreService.Client.GetSystemWideListXml(new UsersFilterData { BaseColumns = ListBaseColumns.IdAndTitle, IsPredefined = false });
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(listXml.ToString());
                output = xmlOrJson.ToString().ToLower() == "json" ? JsonConvert.SerializeObject(doc) : doc.ToString();
                return doc.InnerXml;

            }
            catch (Exception ex)
            {              
                return ex.Message.ToString();
            }

        }
        #endregion

        #region GetListOfAllFolder
        public string GetListOfAllFolder(string tcmuri)
        {
            try
            {
                coreService = CoreServiceFactory.GetCoreServiceContext(new Uri(ConfigurationManager.AppSettings["CoreServiceURL"].ToString()), new NetworkCredential(ConfigurationManager.AppSettings["UserName"].ToString(), ConfigurationManager.AppSettings["Password"].ToString(), ConfigurationManager.AppSettings["Domain"].ToString()));
                string output = string.Empty;
                var rootFolderUri = "tcm:" + tcmuri;
                OrganizationalItemItemsFilterData filter = new OrganizationalItemItemsFilterData();
                var listXml = coreService.Client.GetListXml(rootFolderUri, filter);
                return listXml.ToString();

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

        }
        #endregion
    }
}
