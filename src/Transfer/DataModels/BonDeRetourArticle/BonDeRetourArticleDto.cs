using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grs.BioRestock.Transfer.DataModels.BonDeRetourArticle
{
    public class BonDeRetourArticleDto
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public int BonDeretourId { get; set; }
        public int Quantity { get; set; }
    }
}
