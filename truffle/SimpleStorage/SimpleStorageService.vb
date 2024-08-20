Imports System
Imports System.Threading.Tasks
Imports System.Collections.Generic
Imports System.Numerics
Imports Nethereum.Hex.HexTypes
Imports Nethereum.ABI.FunctionEncoding.Attributes
Imports Nethereum.Web3
Imports Nethereum.RPC.Eth.DTOs
Imports Nethereum.Contracts.CQS
Imports Nethereum.Contracts.ContractHandlers
Imports Nethereum.Contracts
Imports System.Threading
Imports Truffle.Contracts.SimpleStorage.ContractDefinition
Namespace Truffle.Contracts.SimpleStorage


    Public Partial Class SimpleStorageService
    
    
        Public Shared Function DeployContractAndWaitForReceiptAsync(ByVal web3 As Nethereum.Web3.Web3, ByVal simpleStorageDeployment As SimpleStorageDeployment, ByVal Optional cancellationTokenSource As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return web3.Eth.GetContractDeploymentHandler(Of SimpleStorageDeployment)().SendRequestAndWaitForReceiptAsync(simpleStorageDeployment, cancellationTokenSource)
        
        End Function
         Public Shared Function DeployContractAsync(ByVal web3 As Nethereum.Web3.Web3, ByVal simpleStorageDeployment As SimpleStorageDeployment) As Task(Of String)
        
            Return web3.Eth.GetContractDeploymentHandler(Of SimpleStorageDeployment)().SendRequestAsync(simpleStorageDeployment)
        
        End Function
        Public Shared Async Function DeployContractAndGetServiceAsync(ByVal web3 As Nethereum.Web3.Web3, ByVal simpleStorageDeployment As SimpleStorageDeployment, ByVal Optional cancellationTokenSource As CancellationTokenSource = Nothing) As Task(Of SimpleStorageService)
        
            Dim receipt = Await DeployContractAndWaitForReceiptAsync(web3, simpleStorageDeployment, cancellationTokenSource)
            Return New SimpleStorageService(web3, receipt.ContractAddress)
        
        End Function
    
        Protected Property Web3 As Nethereum.Web3.IWeb3
        
        Public Property ContractHandler As ContractHandler
        
        Public Sub New(ByVal web3 As Nethereum.Web3.Web3, ByVal contractAddress As String)
            Web3 = web3
            ContractHandler = web3.Eth.GetContractHandler(contractAddress)
        End Sub
    
        Public Sub New(ByVal web3 As Nethereum.Web3.IWeb3, ByVal contractAddress As String)
            Web3 = web3
            ContractHandler = web3.Eth.GetContractHandler(contractAddress)
        End Sub
    
        Public Function ReadQueryAsync(ByVal readFunction As ReadFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of ReadFunction, BigInteger)(readFunction, blockParameter)
        
        End Function

        
        Public Function ReadQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            return ContractHandler.QueryAsync(Of ReadFunction, BigInteger)(Nothing, blockParameter)
        
        End Function



        Public Function WriteRequestAsync(ByVal writeFunction As WriteFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of WriteFunction)(writeFunction)
        
        End Function

        Public Function WriteRequestAndWaitForReceiptAsync(ByVal writeFunction As WriteFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of WriteFunction)(writeFunction, cancellationToken)
        
        End Function

        
        Public Function WriteRequestAsync(ByVal [newValue] As BigInteger) As Task(Of String)
        
            Dim writeFunction = New WriteFunction()
                writeFunction.NewValue = [newValue]
            
            Return ContractHandler.SendRequestAsync(Of WriteFunction)(writeFunction)
        
        End Function

        
        Public Function WriteRequestAndWaitForReceiptAsync(ByVal [newValue] As BigInteger, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim writeFunction = New WriteFunction()
                writeFunction.NewValue = [newValue]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of WriteFunction)(writeFunction, cancellationToken)
        
        End Function
    
    End Class

End Namespace
