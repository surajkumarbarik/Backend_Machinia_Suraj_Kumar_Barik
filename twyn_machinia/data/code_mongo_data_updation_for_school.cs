
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace center_of_school.data
{

    public class Address
    {
        public string? DetailedAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Pincode { get; set; }
    }


    public class code_for_mongo_operation
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        public string? CenterName { get; set; }

        public string CenterCode { get; set; }

        public Address? Address { get; set; }

        public int StudentCapacity { get; set; }

        public List<string>? CoursesOffered { get; set; }

        public long CreatedOn { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        public string? ContactEmail { get; set; }

        public string? ContactPhone { get; set; }

    }
}
