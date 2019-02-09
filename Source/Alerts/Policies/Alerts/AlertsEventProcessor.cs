using System.Collections.Generic;
using System.Linq;
using Dolittle.Events.Processing;
using Dolittle.ReadModels;
using Events.Alerts;
using Read.CaseReports;
using Read.DataCollectors;
using Read.DataOwners;

namespace Policies.Alerts
{
    public class AlertsEventProcessor : ICanProcessEvents
    {
        private readonly IMailSender _mailSender;
        private readonly IReadModelRepositoryFor<DataOwner> _dataOwnersRepository;
        private readonly IReadModelRepositoryFor<Case> _casesRepository;
        private readonly IReadModelRepositoryFor<DataCollector> _dataCollectorRepository;

        public AlertsEventProcessor(
            IMailSender mailSender,
            IReadModelRepositoryFor<DataOwner> dataOwnersRepository,
            IReadModelRepositoryFor<Case> casesRepository,
            IReadModelRepositoryFor<DataCollector> dataCollectorRepository)
        {
            this._mailSender = mailSender;
            this._dataOwnersRepository = dataOwnersRepository;
            this._casesRepository = casesRepository;
            _dataCollectorRepository = dataCollectorRepository;
        }

        [EventProcessor("042ec98c-ed13-061c-f175-c15b3a9363f2")]
        public void Process(AlertOpened @event)
        {
            var owners = _dataOwnersRepository.Query.ToList();
            var caseItems = @event.Cases.Select(c => _casesRepository.GetById(c));
            var caseItem = caseItems.First();

            var orderedCollectors = caseItems.GroupBy(c => c.DataCollectorId).OrderByDescending(g => g.Count());
            var dataCollectors = orderedCollectors.Select(g => new { incidents = g.Count(), collector = _dataCollectorRepository.GetById(g.Key) });

            var collectorsDescription = dataCollectors.Select(c => $"{c.collector.FullName}, phone:{string.Join(",", c.collector.PhoneNumbers)}, number of cases: {c.incidents} ");

            foreach (var owner in owners)
            {
                var message = $"Dear {owner.Name},\nAlert opened on health risk {caseItem.HealthRiskNumber} with {@event.Cases.Length} case(s). Please follow up using the Reporting module in CBS.\n";
                message += collectorsDescription.Select(d => $"{d}\n");
                _mailSender.Send(owner.Email, $"CBS Alert opened", message);
            }
        }

    }
}