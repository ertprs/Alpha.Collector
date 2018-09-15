using System.Collections.Generic;

namespace Alpha.Collector.Web.Infrastructure.Razor
{
    public class TreeModel
    { 
        public string id { get; set; }
        public string text { get; set; }

        public TreeState state { get; set; } 
         

        public List<TreeModel> children { get; set; }
    }

    public class TreeState {
        public bool opened { get; set; }

        public bool disabled { get; set; }

        public bool selected { get; set; }
    }

    public class TreeAttributes {
        public int id { get; set; }
    }

   public class Tree
    {
        public TreeAttributes attributes { get; set; }

    }
}