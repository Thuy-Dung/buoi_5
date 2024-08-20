using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace Truffle.Contracts.SimpleStorage.ContractDefinition
{


    public partial class SimpleStorageDeployment : SimpleStorageDeploymentBase
    {
        public SimpleStorageDeployment() : base(BYTECODE) { }
        public SimpleStorageDeployment(string byteCode) : base(byteCode) { }
    }

    public class SimpleStorageDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "6080604052348015600f57600080fd5b5060ac8061001e6000396000f3fe6080604052348015600f57600080fd5b506004361060325760003560e01c80632f048afa14603757806357de26a4146049575b600080fd5b60476042366004605e565b600055565b005b60005460405190815260200160405180910390f35b600060208284031215606f57600080fd5b503591905056fea2646970667358221220aed66f8c7b39a234e37e167703e752c6fa084fa72d99369af1634d03dc859a1164736f6c63430008130033";
        public SimpleStorageDeploymentBase() : base(BYTECODE) { }
        public SimpleStorageDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class ReadFunction : ReadFunctionBase { }

    [Function("read", "uint256")]
    public class ReadFunctionBase : FunctionMessage
    {

    }

    public partial class WriteFunction : WriteFunctionBase { }

    [Function("write")]
    public class WriteFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "newValue", 1)]
        public virtual BigInteger NewValue { get; set; }
    }

    public partial class ReadOutputDTO : ReadOutputDTOBase { }

    [FunctionOutput]
    public class ReadOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }


}
