namespace QuestionnaireTask
{
    public class MiaoTokenAbi
    {
        public const string ContractAddress = @"0x952Feb77b9986D1cC64799C6721D6459eAae7b94";
        //public const string ContractAddress = @"0x97e809e402191BA9b90dbf17D8484fbc11A5a95F";

        public const string Approval = @" [{ 'inputs': [
        {
          'indexed': true,
          'internalType': 'address',
          'name': 'owner',
          'type': 'address'
        },
        {
          'indexed': true,
          'internalType': 'address',
          'name': 'spender',
          'type': 'address'
        },
        {
          'indexed': false,
          'internalType': 'uint256',
          'name': 'value',
          'type': 'uint256'
        }
      ],
      'name': 'Approval',
      'type': 'event'}]";

        public const string Transfer = @"    [{'inputs': [
        {
          'indexed': true,
          'internalType': 'address',
          'name': 'from',
          'type': 'address'
        },
        {
          'indexed': true,
          'internalType': 'address',
          'name': 'to',
          'type': 'address'
        },
        {
          'indexed': false,
          'internalType': 'uint256',
          'name': 'value',
          'type': 'uint256'
        }
      ],
      'name': 'Transfer',
      'type': 'event'}]";

        public const string Mint = @" [{'inputs': [
        {
          'internalType': 'address',
          'name': 'account',
          'type': 'address'
        },
        {
          'internalType': 'uint256',
          'name': 'amount',
          'type': 'uint256'
        }
      ],
      'name': 'mint',
      'outputs': [],
      'stateMutability': 'nonpayable',
      'type': 'function'}]";
        public const string Burn = @" [{ 'inputs': [
        {
          'internalType': 'address',
          'name': 'account',
          'type': 'address'
        },
        {
          'internalType': 'uint256',
          'name': 'amount',
          'type': 'uint256'
        }
      ],
      'name': 'burn',
      'outputs': [],
      'stateMutability': 'nonpayable',
      'type': 'function'}]";

        public const string BalanceOf = @"[{  'inputs': [
        {
          'internalType': 'address',
          'name': 'account',
          'type': 'address'
        }
      ],
      'name': 'balanceOf',
      'outputs': [
        {
          'internalType': 'uint256',
          'name': '',
          'type': 'uint256'
        }
      ],
      'stateMutability': 'view',
      'type': 'function',
      'constant': true}]";

        public const string Purchase = @"[{ 'inputs': [
        {
          'internalType': 'uint256',
          'name': '_value',
          'type': 'uint256'
        },
        {
          'internalType': 'uint256',
          'name': 'exchangeRate',
          'type': 'uint256'
        }
      ],
      'name': 'purchase',
      'outputs': [],
      'stateMutability': 'payable',
      'type': 'function',
      'payable': true}]";

        public const string WithDraw = @"[{
      'inputs': [],
      'name': 'withdraw',
      'outputs': [],
      'stateMutability': 'nonpayable',
      'type': 'function'
    }]";
        public const string PurchaseToken = @"[ {
      'anonymous': false,
      'inputs': [
        {
          'indexed': true,
          'internalType': 'address',
          'name': 'from',
          'type': 'address'
        },
        {
          'indexed': true,
          'internalType': 'address',
          'name': 'to',
          'type': 'address'
        },
        {
          'indexed': false,
          'internalType': 'uint256',
          'name': 'value',
          'type': 'uint256'
        },
        {
          'indexed': false,
          'internalType': 'uint256',
          'name': 'rate',
          'type': 'uint256'
        }
      ],
      'name': 'PurchaseToken',
      'type': 'event'
    }]";

    }
}
