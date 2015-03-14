using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC_With_MongoDb.Models
{
    public class EmployesModel
    {
        public ObjectId _id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string empname { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "Email is not valid")]
        public string email { get; set; }
        [Required(ErrorMessage = "Address is Required")]
        public string address { get; set; }
        [Required(ErrorMessage = "Phon No is Required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Phon No is not valid.")]
        public string phon { get; set; }

    }
}