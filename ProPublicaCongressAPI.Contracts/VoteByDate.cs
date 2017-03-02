using System;
using System.Collections.Generic;

namespace ProPublicaCongressAPI.Contracts
{
    public class VoteByDate
    {
        public int Congress { get; set; }

        public int Session { get; set; }

        public int RollCallNumber { get; set; }

        public string VoteDetailUrl { get; set; }

        public string BillNumber { get; set; }

        public string Question { get; set; }

        public string Description { get; set; }

        public string VoteType { get; set; }

        public DateTime DateTimeVoted { get; set; }

        public string Result { get; set; }

        public RollCallVoteSummaryDemocratic DemocraticVoteSummary { get; set; }

        public RollCallVoteSummaryRepublican RepublicanVoteSummary { get; set; }

        public RollCallVoteSummaryIndependent IndependentVoteSummary { get; set; }

        public RollCallVoteSummaryTotal TotalVoteSummary { get; set; }
    }
}