using PhotoSharing.DB_EntitySQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoSharing.ViewModel
{
    public class PhotoComments
    {
        public List<Photos> photos { get; set; }
        public List<Comments> comments { get; set; }
    }
}