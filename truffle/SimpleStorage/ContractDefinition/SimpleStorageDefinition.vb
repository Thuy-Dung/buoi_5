Imports System
Imports System.Threading.Tasks
Imports System.Collections.Generic
Imports System.Numerics
Imports Nethereum.Hex.HexTypes
Imports Nethereum.ABI.FunctionEncoding.Attributes
Imports Nethereum.RPC.Eth.DTOs
Imports Nethereum.Contracts.CQS
Imports Nethereum.Contracts
Imports System.Threading
Namespace Truffle.Contracts.SimpleStorage.ContractDefinition

    
    
    Public Partial Class SimpleStorageDeployment
     Inherits SimpleStorageDeploymentBase
    
        Public Sub New()
            MyBase.New(DEFAULT_BYTECODE)
        End Sub
        
        Public Sub New(ByVal byteCode As String)
            MyBase.New(byteCode)
        End Sub
    
    End Class

    Public Class SimpleStorageDeploymentBase 
            Inherits ContractDeploymentMessage
        
        Public Shared DEFAULT_BYTECODE As String = "6080604052348015600f57600080fd5b5060ac8061001e6000396000f3fe6080604052348015600f57600080fd5b506004361060325760003560e01c80632f048afa14603757806357de26a4146049575b600080fd5b60476042366004605e565b600055565b005b60005460405190815260200160405180910390f35b600060208284031215606f57600080fd5b503591905056fea2646970667358221220aed66f8c7b39a234e37e167703e752c6fa084fa72d99369af1634d03dc859a1164736f6c63430008130033"
        
        Public Sub New()
            MyBase.New(DEFAULT_BYTECODE)
        End Sub
        
        Public Sub New(ByVal byteCode As String)
            MyBase.New(byteCode)
        End Sub
        

    
    End Class    
    
    Public Partial Class ReadFunction
        Inherits ReadFunctionBase
    End Class

        <[Function]("read", "uint256")>
    Public Class ReadFunctionBase
        Inherits FunctionMessage
    

    
    End Class
    
    
    Public Partial Class WriteFunction
        Inherits WriteFunctionBase
    End Class

        <[Function]("write")>
    Public Class WriteFunctionBase
        Inherits FunctionMessage
    
        <[Parameter]("uint256", "newValue", 1)>
        Public Overridable Property [NewValue] As BigInteger
    
    End Class
    
    
    Public Partial Class ReadOutputDTO
        Inherits ReadOutputDTOBase
    End Class

    <[FunctionOutput]>
    Public Class ReadOutputDTOBase
        Implements IFunctionOutputDTO
        
        <[Parameter]("uint256", "", 1)>
        Public Overridable Property [ReturnValue1] As BigInteger
    
    End Class    
    

End Namespace
