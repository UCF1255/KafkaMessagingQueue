using System;
using System.Collections.Generic;
using Confluent.Kafka;
using DataStreaming.RequestModel;
using DataStreaming.ViewModel;
using Newtonsoft.Json;

namespace DataStreaming
{
   public class ESLogProducer : IESLogProducer
    {
        public  void Produce() // string message
        {
            SuperLog superLog = new SuperLog
            {
                agentRequestModel = new AgentRequestModel
                {
                    agencyName = "MGAgency",
                    agentCode = 12345678,
                    agentName = "Mayank Khanna"
                },
                log = new LOG
                {

                    logName = "Elastic Search Log",
                    logDetail = "ES Details Data"
                }
            };

            Task task = new Task
            {
                TaskName = "TOPIC_TESTEL",
                TopicName = "TOPIC_TEST",
                Parameter = JsonConvert.SerializeObject(superLog)
            };

            var producerConfig = new Dictionary<string, string> { { "bootstrap.servers", "localhost:9092" } };

            using (var producer = new ProducerBuilder<Null, string>(producerConfig)
                .SetKeySerializer(Serializers.Null)
                .SetValueSerializer(Serializers.Utf8)
                .Build())
            {
                var result = producer.ProduceAsync(task.TopicName, new Message<Null, string> { Value = JsonConvert.SerializeObject(task) }).GetAwaiter().GetResult();
                Console.WriteLine("Response: P{0}, O{1} : {2}", result.Partition.Value, result.Offset.Value, result.Value);
                Console.ReadLine();
            }
        }
    }
}
