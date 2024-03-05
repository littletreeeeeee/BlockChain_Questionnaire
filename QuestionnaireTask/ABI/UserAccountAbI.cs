namespace QuestionnaireTask
{
    public class UserAccountAbI
    {
        public const string ContractAddress = @"0x14481C3C5c1c817B96A34630DcECefF4f71da1fF";

        public const string CreateAccount = @"[{
      'inputs': [
        {
          'internalType': 'string',
          'name': '_id',
          'type': 'string'
        },
        {
          'internalType': 'string',
          'name': 'ipfsAddress',
          'type': 'string'
        }
      ],
      'name': 'CreateAccount',
      'outputs': [],
      'stateMutability': 'nonpayable',
      'type': 'function'
    }]";

        public const string GetAccount = @"[{
      'inputs': [
        {
          'internalType': 'string',
          'name': '_id',
          'type': 'string'
        }
      ],
      'name': 'GetAccountData',
      'outputs': [
        {
          'internalType': 'string',
          'name': '',
          'type': 'string'
        }
      ],
      'stateMutability': 'view',
      'type': 'function',
      'constant': true
    }]";
        public const string ModifyAccountLog = @"[{
      'anonymous': false,
      'inputs': [
        {
          'indexed': false,
          'internalType': 'string',
          'name': 'userId',
          'type': 'string'
        },
        {
          'indexed': false,
          'internalType': 'string',
          'name': 'ipfsAddress',
          'type': 'string'
        }
      ],
      'name': 'ModifyAccountLog',
      'type': 'event'
    }]";



    }


}
