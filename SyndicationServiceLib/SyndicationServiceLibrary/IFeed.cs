﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.ServiceModel.Web;
using System.Text;

namespace SyndicationServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IFeed1" in both code and config file together.
    [ServiceContract]
    [ServiceKnownType(typeof(Atom10FeedFormatter))]
    [ServiceKnownType(typeof(Rss20FeedFormatter))]
    public interface IFeed
    {

        [OperationContract]
        [WebGet(UriTemplate = "*", BodyStyle = WebMessageBodyStyle.Bare)]
        SyndicationFeedFormatter CreateFeed();

        [OperationContract]
        [WebGet(UriTemplate = "students/{studentId}/notes", BodyStyle = WebMessageBodyStyle.Bare)]
        SyndicationFeedFormatter GetStudentNotes(string studentId);

        // TODO: Add your service operations here
    }
}
