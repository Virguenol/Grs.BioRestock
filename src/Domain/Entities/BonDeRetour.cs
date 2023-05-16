﻿using Grs.BioRestock.Domain.Contracts;
using Grs.BioRestock.Shared.Enums;
using Grs.BioRestock.Shared.Enums.BonDeRetour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnomalyType = Grs.BioRestock.Shared.Enums.AnomalyType;

namespace Grs.BioRestock.Domain.Entities
{
    public class BonDeRetour : AuditableEntity<int>
    {
        public string Code { get; set; }
        public int CustomerId { get; set; }
        public AnomalyType AnomalyType { get; set; } = AnomalyType.Produit_Introuvable;
        public string Reference { get; set; }
        public BonDeRetourStatus Status { get; set; } = BonDeRetourStatus.Nouveau;
        public BonDeRetourDepot Depot { get; set; } = BonDeRetourDepot.Casablanca;
        public List<Customer> Customers { get; set; }
    }
}
