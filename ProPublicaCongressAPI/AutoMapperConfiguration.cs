using AutoMapper;
using ProPublicaCongressAPI.Resolvers;
using System;

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
                            return CreateDateTimeFromDateAndTime(source.DateVoted, source.TimeVoted);
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
                            return CreateDateTimeFromDateAndTime(source.DateRollCall, source.TimeRollCall);
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
                            return CreateDateTimeFromDateAndTime(source.DateVoted, source.TimeVoted);
                        }));
                    x.CreateMap<InternalModels.VoteByDateContainer, Contracts.VoteByDateContainer>();

                    x.CreateMap<InternalModels.SenateNominationVote, Contracts.SenateNominationVote>()
                        .ForMember(dest => dest.DateTimeVoted, opts => opts.ResolveUsing(source =>
                        {
                            return CreateDateTimeFromDateAndTime(source.DateVoted, source.TimeVoted);
                        }));
                    x.CreateMap<InternalModels.SenateNominationVoteContainer, Contracts.SenateNominationVoteContainer>();

                    x.CreateMap<InternalModels.RecentBill, Contracts.RecentBill>()
                        .ForMember(dest => dest.DateIntroduced,
                            opts => opts.ResolveUsing<DateTimeResolver, string>(s => s.DateIntroduced))
                        .ForMember(dest => dest.DateLatestMajorAction,
                            opts => opts.ResolveUsing<DateTimeResolver, string>(s => s.DateLatestMajorAction));
                    x.CreateMap<InternalModels.RecentBillsContainer, Contracts.RecentBillsContainer>();

                    x.CreateMap<InternalModels.RecentBillByMember, Contracts.RecentBillByMember>()
                        .ForMember(dest => dest.DateIntroduced,
                            opts => opts.ResolveUsing<DateTimeResolver, string>(s => s.DateIntroduced))
                        .ForMember(dest => dest.DateLatestMajorAction,
                            opts => opts.ResolveUsing<DateTimeResolver, string>(s => s.DateLatestMajorAction));
                    x.CreateMap<InternalModels.RecentBillsByMemberContainer, Contracts.RecentBillsByMemberContainer>();

                    x.CreateMap<InternalModels.SpecificBillVoteSummary, Contracts.SpecificBillVoteSummary>()
                        .ForMember(dest => dest.DateTimeVoted, opts => opts.ResolveUsing(source =>
                        {
                            return CreateDateTimeFromDateAndTime(source.DateVoted, source.TimeVoted);
                        }));
                    x.CreateMap<InternalModels.SpecificBillAction, Contracts.SpecificBillAction>()
                        .ForMember(dest => dest.DateTimeOccurred,
                            opts => opts.ResolveUsing<DateTimeResolver, string>(s => s.DateTimeOccurred));
                    x.CreateMap<InternalModels.SpecificBill, Contracts.SpecificBill>()
                        .ForMember(dest => dest.DateIntroduced,
                            opts => opts.ResolveUsing<DateTimeResolver, string>(s => s.DateIntroduced))
                        .ForMember(dest => dest.DateHousePassageVote,
                            opts => opts.ResolveUsing<NullableDateTimeResolver, string>(s => s.DateHousePassageVote))
                        .ForMember(dest => dest.DateSenatePassageVote,
                            opts => opts.ResolveUsing<NullableDateTimeResolver, string>(s => s.DateSenatePassageVote))
                        .ForMember(dest => dest.DateLatestMajorAction, 
                            opts => opts.ResolveUsing<DateTimeResolver, string>(s => s.DateLatestMajorAction));

                    x.CreateMap<InternalModels.SpecificBillDetailRelated, Contracts.SpecificBillDetailRelated>()
                        .ForMember(dest => dest.DateIntroduced,
                            opts => opts.ResolveUsing<DateTimeResolver, string>(s => s.DateIntroduced))
                        .ForMember(dest => dest.DateLatestMajorAction,
                            opts => opts.ResolveUsing<DateTimeResolver, string>(s => s.DateLatestMajorAction));
                    x.CreateMap<InternalModels.SpecificBillDetailSubject, Contracts.SpecificBillDetailSubject>();
                    x.CreateMap<InternalModels.SpecificBillDetailAmendment, Contracts.SpecificBillDetailAmendment>()
                        .ForMember(dest => dest.DateIntroduced,
                            opts => opts.ResolveUsing<DateTimeResolver, string>(s => s.DateIntroduced))
                        .ForMember(dest => dest.DateLatestMajorAction,
                            opts => opts.ResolveUsing<DateTimeResolver, string>(s => s.DateLatestMajorAction));
                    x.CreateMap<InternalModels.SpecificBillDetail, Contracts.SpecificBillDetail>()
                        .ForMember(dest => dest.DateIntroduced,
                            opts => opts.ResolveUsing<DateTimeResolver, string>(s => s.DateIntroduced))
                        .ForMember(dest => dest.DateHousePassageVote,
                            opts => opts.ResolveUsing<NullableDateTimeResolver, string>(s => s.DateHousePassageVote))
                        .ForMember(dest => dest.DateLatestMajorAction,
                            opts => opts.ResolveUsing<DateTimeResolver, string>(s => s.DateLatestMajorAction))
                        .ForMember(dest => dest.DateSenatePassageVote,
                            opts => opts.ResolveUsing<NullableDateTimeResolver, string>(s => s.DateSenatePassageVote));

                    x.CreateMap<InternalModels.BillCosponsor, Contracts.BillCosponsor>()
                        .ForMember(dest => dest.DateCosponsored,
                            opts => opts.ResolveUsing<DateTimeResolver, string>(s => s.DateCosponsored));
                    x.CreateMap<InternalModels.BillCosponsorPartySummary, Contracts.BillCosponsorPartySummary>();
                    x.CreateMap<InternalModels.BillCosponsorContainer, Contracts.BillCosponsorContainer>()
                        .ForMember(dest => dest.DateIntroduced,
                            opts => opts.ResolveUsing<DateTimeResolver, string>(s => s.DateIntroduced))
                        .ForMember(dest => dest.DateHousePassageVote,
                            opts => opts.ResolveUsing<NullableDateTimeResolver, string>(s => s.DateHousePassageVote))
                        .ForMember(dest => dest.DateLatestMajorAction,
                            opts => opts.ResolveUsing<DateTimeResolver, string>(s => s.DateLatestMajorAction))
                        .ForMember(dest => dest.DateSenatePassageVote,
                            opts => opts.ResolveUsing<NullableDateTimeResolver, string>(s => s.DateSenatePassageVote));

                    x.CreateMap<InternalModels.RecentNomination, Contracts.RecentNomination>();
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

        private static DateTime CreateDateTimeFromDateAndTime(string date, string time)
        {
            string rawDateTime = date;

            if (!String.IsNullOrWhiteSpace(time))
            {
                rawDateTime += " " + time;
            }

            DateTime parsedDateTime;
            DateTime.TryParse(rawDateTime, out parsedDateTime);

            return parsedDateTime;
        }

        public static void Reset()
        {
            config = null;
            mapper = null;
        }
    }
}