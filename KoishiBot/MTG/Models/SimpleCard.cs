using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishiBot.MTG.Models
{
    public class SimpleCard
    {

        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string CardUrl { get; set; }

        public SimpleCard(string name, string imgUrl, string url)
        {
            this.Name = name;
            this.ImageUrl = imgUrl;
            this.CardUrl = url;
        }
    }
}
