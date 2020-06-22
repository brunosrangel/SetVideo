using System;
namespace setVideo.Model
{
    public class Location
    {
       public int id { get; set; }
       public virtual Movie movie { get; set; }
       public virtual Customer Customer { get; set; }
       public DateTime locationDate { get; set; }
        public DateTime locationDevolution { get; set; }
        public DateTime locationReturned { get; set; }
        

    }
}
