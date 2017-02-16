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