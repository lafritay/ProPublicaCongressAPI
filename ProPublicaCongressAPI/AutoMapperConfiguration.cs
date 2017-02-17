using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProPublicaCongressAPI
{
    internal static class AutoMapperConfiguration
    {
        private static bool isInitialized = false;
        private static MapperConfiguration config;
        private static IMapper mapper;

        public static IMapper Mapper { get { return mapper; } }

        public static void Initialize()
        {
            if (isInitialized)
            {
                return;
            }

            if (config == null)
            {
                config = new MapperConfiguration(x =>
                {
                    x.CreateMap<InternalModels.MemberSummary, Contracts.MemberSummary>();
                    x.CreateMap<InternalModels.MembersContainer, Contracts.MembersContainer>();

                    x.CreateMap<InternalModels.MemberCommittee, Contracts.MemberCommittee>();
                    x.CreateMap<InternalModels.MemberRole, Contracts.MemberRole>();
                    x.CreateMap<InternalModels.Member, Contracts.Member>();

                    x.CreateMap<InternalModels.NewMember, Contracts.NewMember>();
                    x.CreateMap<InternalModels.NewMembersContainer, Contracts.NewMembersContainer>();

                    x.CreateMap<InternalModels.CurrentMember, Contracts.CurrentMember>();

                    x.CreateMap<InternalModels.MemberVoteBill, Contracts.MemberVoteBill>();
                    x.CreateMap<InternalModels.MemberVote, Contracts.MemberVote>()
                        .ForMember(dest => dest.DateTimeVoted, opts => opts.ResolveUsing(source =>
                        {
                            string rawDateTimeVoted = source.DateVoted;

                            if(!String.IsNullOrWhiteSpace(source.TimeVoted))
                            {
                                rawDateTimeVoted += " " + source.TimeVoted;
                            }

                            DateTime dateTimeVoted;
                            DateTime.TryParse(rawDateTimeVoted, out dateTimeVoted);

                            return dateTimeVoted;
                        }));
                    x.CreateMap<InternalModels.MemberVotesContainer, Contracts.MemberVotesContainer>();

                    x.CreateMap<InternalModels.MemberVoteComparison, Contracts.MemberVoteComparison>();

                    x.CreateMap<InternalModels.MemberBillSponsorshipComparison, Contracts.MemberBillSponsorshipComparison>();
                    x.CreateMap<InternalModels.MemberBillSponsorshipComparisonContainer, Contracts.MemberBillSponsorshipComparisonContainer>();

                    x.CreateMap<InternalModels.MemberBillCosponsored, Contracts.MemberBillCosponsored>();
                    x.CreateMap<InternalModels.MemberBillsCosponsoredContainer, Contracts.MemberBillsCosponsoredContainer>();

                    x.CreateMap<InternalModels.RollCallVotePosition, Contracts.RollCallVotePosition>();
                    x.CreateMap<InternalModels.RollCallVote, Contracts.RollCallVote>()
                        .ForMember(dest => dest.DateTimeRollCall, opts => opts.ResolveUsing(source =>
                        {
                            string rawDateTimeVoted = source.DateRollCall;

                            if (!String.IsNullOrWhiteSpace(source.TimeRollCall))
                            {
                                rawDateTimeVoted += " " + source.TimeRollCall;
                            }

                            DateTime dateTimeVoted;
                            DateTime.TryParse(rawDateTimeVoted, out dateTimeVoted);

                            return dateTimeVoted;
                        }));
                    x.CreateMap<InternalModels.RollCallVoteSummaryDemocratic, Contracts.RollCallVoteSummaryDemocratic>();
                    x.CreateMap<InternalModels.RollCallVoteSummaryRepublican, Contracts.RollCallVoteSummaryRepublican>();
                    x.CreateMap<InternalModels.RollCallVoteSummaryIndependent, Contracts.RollCallVoteSummaryIndependent>();
                    x.CreateMap<InternalModels.RollCallVoteSummaryTotal, Contracts.RollCallVoteSummaryTotal>();
                    x.CreateMap<InternalModels.RollCallVoteContainer, Contracts.RollCallVoteContainer>();
                    x.CreateMap<InternalModels.RollCallVotesContainer, Contracts.RollCallVotesContainer>();

                    x.CreateMap<InternalModels.VoteByTypeMember, Contracts.VoteByTypeMember>();
                    x.CreateMap<InternalModels.VoteByTypeContainer, Contracts.VoteByTypeContainer>();

                    x.CreateMap<InternalModels.VoteByDate, Contracts.VoteByDate>()
                        .ForMember(dest => dest.DateTimeVoted, opts => opts.ResolveUsing(source =>
                        {
                            string rawDateTimeVoted = source.DateVoted;

                            if (!String.IsNullOrWhiteSpace(source.TimeVoted))
                            {
                                rawDateTimeVoted += " " + source.TimeVoted;
                            }

                            DateTime dateTimeVoted;
                            DateTime.TryParse(rawDateTimeVoted, out dateTimeVoted);

                            return dateTimeVoted;
                        }));
                    x.CreateMap<InternalModels.VoteByDateContainer, Contracts.VoteByDateContainer>();
                });
            }

            if (mapper == null)
            {
                mapper = config.CreateMapper();
            }

            isInitialized = true;

#if DEBUG
            config.AssertConfigurationIsValid();
#endif
        }

        public static void Reset()
        {
            config = null;
            mapper = null;
        }
    }
}