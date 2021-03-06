﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using SharpArtileri.Services;
using Ninject;

namespace SharpArtileri.Web
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AjaxService
    {
        [Inject]
        public ItemProvider ItemService { get; set; }

        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract]        
        public decimal GetDefaultPrice(int supplierID, int itemID, string unitName)
        {
            return ItemService.GetDefaultPrice(supplierID, itemID, unitName);
        }

        // Add more operations here and mark them with [OperationContract]
    }
}
