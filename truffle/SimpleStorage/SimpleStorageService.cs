using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using Truffle.Contracts.SimpleStorage.ContractDefinition;

namespace Truffle.Contracts.SimpleStorage
{
    public partial class SimpleStorageService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, SimpleStorageDeployment simpleStorageDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<SimpleStorageDeployment>().SendRequestAndWaitForReceiptAsync(simpleStorageDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, SimpleStorageDeployment simpleStorageDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<SimpleStorageDeployment>().SendRequestAsync(simpleStorageDeployment);
        }

        public static async Task<SimpleStorageService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, SimpleStorageDeployment simpleStorageDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, simpleStorageDeployment, cancellationTokenSource);
            return new SimpleStorageService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public SimpleStorageService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public SimpleStorageService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<BigInteger> ReadQueryAsync(ReadFunction readFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ReadFunction, BigInteger>(readFunction, blockParameter);
        }

        
        public Task<BigInteger> ReadQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ReadFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> WriteRequestAsync(WriteFunction writeFunction)
        {
             return ContractHandler.SendRequestAsync(writeFunction);
        }

        public Task<TransactionReceipt> WriteRequestAndWaitForReceiptAsync(WriteFunction writeFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(writeFunction, cancellationToken);
        }

        public Task<string> WriteRequestAsync(BigInteger newValue)
        {
            var writeFunction = new WriteFunction();
                writeFunction.NewValue = newValue;
            
             return ContractHandler.SendRequestAsync(writeFunction);
        }

        public Task<TransactionReceipt> WriteRequestAndWaitForReceiptAsync(BigInteger newValue, CancellationTokenSource cancellationToken = null)
        {
            var writeFunction = new WriteFunction();
                writeFunction.NewValue = newValue;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(writeFunction, cancellationToken);
        }
    }
}
