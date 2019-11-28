using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.ServiceModel.Web;
using System.Text;
using Newtonsoft.Json;

namespace SyndicationServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Feed1" in both code and config file together.
    public class Feed : IFeed
    { 

        public SyndicationFeedFormatter CreateFeed()
        {
            // Create a new Syndication Feed.
            SyndicationFeed feed = new SyndicationFeed(
                title: "Feed Title", 
                description:"A WCF Syndication Feed", 
                feedAlternateLink: new Uri("http://localhost:8733/Design_Time_Addresses/SyndicationServiceLibrary/Feed/"),
                id: "id",
                lastUpdatedTime: new DateTimeOffset(DateTime.Now),
                items: GetNewsItems()
            );

            // Return ATOM or RSS based on query string
            // rss -> http://localhost:8733/Design_Time_Addresses/SyndicationServiceLibrary/Feed/
            // atom -> http://localhost:8733/Design_Time_Addresses/SyndicationServiceLibrary/Feed/?format=atom
            string query = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["format"];

            SyndicationFeedFormatter formatter = null;
            if (query == "atom")
            {
                formatter = new Atom10FeedFormatter(feed);
            }
            else
            {
                formatter = new Rss20FeedFormatter(feed);
            }

            return formatter;
        }

        private List<SyndicationItem> GetNewsItems()
        {
            List<SyndicationItem> rc = new List<SyndicationItem>(new List<SyndicationItem>()
                                                                    {
                                                                        new SyndicationItem(
                                                                            "Item One",
                                                                            "This is the content for item one",
                                                                            new Uri("http://localhost/One"),
                                                                            "ItemOneID",
                                                                            DateTime.Now),
                                                                        new SyndicationItem(
                                                                            "Item Two",
                                                                            "This is the content for item two",
                                                                            new Uri("http://localhost/Two"),
                                                                            "ItemTwoID",
                                                                            DateTime.Now)

                                                                    }
            );
            return rc;
        }


        public SyndicationFeedFormatter GetStudentNotes(string studentId)
        {
            SyndicationFeed feed = new SyndicationFeed(
                "Subjects & Notes",
                "Get list of notes by all subjects for this student",
                null
            );
            List<SyndicationItem> items = new List<SyndicationItem>();
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://localhost:44357/WcfDataService1.svc/Notes?$filter=(StudentId%20eq%20" + studentId + ")&$format=json");
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string responseString = reader.ReadToEnd();
            var notesResp = JsonConvert.DeserializeObject<NoteResponse>(responseString);
            var notes = notesResp.Value;
            foreach (var note in notes)
            {
                items.Add(new SyndicationItem(note.Subj, note.Note1.ToString(), null));
            }
            feed.Items = items;

            string query = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["format"];
            SyndicationFeedFormatter formatter = null;
            if (query == "atom")
            {
                formatter = new Atom10FeedFormatter(feed);
            }
            else
            {
                formatter = new Rss20FeedFormatter(feed);
            }
            return formatter;
        }

    }
}
