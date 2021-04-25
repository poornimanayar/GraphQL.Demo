using GraphQL.Execution;
using GraphQL.Language.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Demo.API
{
    public class SerialDocumentExecuter : DocumentExecuter
    {
        private static IExecutionStrategy ParallelExecutionStrategy = new ParallelExecutionStrategy();
        private static IExecutionStrategy SerialExecutionStrategy = new SerialExecutionStrategy();
        private static IExecutionStrategy SubscriptionExecutionStrategy = new SubscriptionExecutionStrategy();

        protected override IExecutionStrategy SelectExecutionStrategy(ExecutionContext context)
        {
            return context.Operation.OperationType switch
            {
                OperationType.Query => ParallelExecutionStrategy,
                OperationType.Mutation => SerialExecutionStrategy,
                OperationType.Subscription => SubscriptionExecutionStrategy,
                _ => base.SelectExecutionStrategy(context)
            };
        }
    }
}
