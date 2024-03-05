namespace QuestionnaireTask
{
    public class QuestionnaireAbi
    {
        public const string ContractAddress = @"0x952Feb77b9986D1cC64799C6721D6459eAae7b94";
        //public const string ContractAddress = @"0x125257D2f2A9a987Bfb317274CC780aec1e304F6";
        public const string CreateSurvey = @"[{'inputs': [
        {
          'internalType': 'string',
          'name': 'docId',
          'type': 'string'
        },
        {
          'internalType': 'uint256',
          'name': 'reward',
          'type': 'uint256'
        },
{
          'internalType': 'uint256',
          'name': 'openReward',
          'type': 'uint256'
        },
        {
          'internalType': 'uint256',
          'name': 'publishQty',
          'type': 'uint256'
        },
        {
          'internalType': 'uint256',
          'name': 'lotteryCount',
          'type': 'uint256'
        },
        {
          'internalType': 'uint256',
          'name': 'lottery',
          'type': 'uint256'
        },
        {
          'internalType': 'uint256',
          'name': 'deadline',
          'type': 'uint256'
        },
        {
          'internalType': 'string',
          'name': 'ipfsAddress',
          'type': 'string'
        }
      ],
      'name': 'createSurvey',
      'outputs': [],
      'stateMutability': 'nonpayable',
      'type': 'function'}]";
        public const string GetSurvey = @"[ {
      'inputs': [
        {
          'internalType': 'string',
          'name': 'docId',
          'type': 'string'
        }
      ],
      'name': 'getSurvey',
      'outputs': [
        {
          'internalType': 'bytes',
          'name': '',
          'type': 'bytes'
        }
      ],
      'stateMutability': 'view',
      'type': 'function',
      'constant': true
    }]";
        public const string FillSurvey = @"[{
      'inputs': [
        {
          'internalType': 'address',
          'name': 'from',
          'type': 'address'
        },
        {
          'internalType': 'string',
          'name': 'docId',
          'type': 'string'
        },
        {
          'internalType': 'string',
          'name': 'ipfsAddress',
          'type': 'string'
        },
 {
          'internalType': 'bool',
          'name': 'isOpen',
          'type': 'bool'
        }
      ],
      'name': 'fillSurvey',
      'outputs': [],
      'stateMutability': 'nonpayable',
      'type': 'function'
    }]";
        public const string GetFillSurvey = @"[ {
      'inputs': [
        {
          'internalType': 'string',
          'name': 'docId',
          'type': 'string'
        },
        {
          'internalType': 'address',
          'name': 'from',
          'type': 'address'
        }
      ],
      'name': 'getFillSurvey',
      'outputs': [
        {
          'internalType': 'bytes',
          'name': '',
          'type': 'bytes'
        }
      ],
      'stateMutability': 'view',
      'type': 'function',
      'constant': true
    }]";
        public const string CreateSurveyLog = @"[     {
      'anonymous': false,
      'inputs': [
        {
          'indexed': false,
          'internalType': 'address',
          'name': 'creatorAddress',
          'type': 'address'
        },
        {
          'indexed': false,
          'internalType': 'uint256',
          'name': 'MiaoTokenForContract',
          'type': 'uint256'
        },
        {
          'indexed': true,
          'internalType': 'address',
          'name': 'ContractAddress',
          'type': 'address'
        },
        {
          'indexed': false,
          'internalType': 'uint256',
          'name': 'MiaoTokenForOwner',
          'type': 'uint256'
        },
        {
          'indexed': true,
          'internalType': 'address',
          'name': 'OwnerAddress',
          'type': 'address'
        }
      ],
      'name': 'CreateSurvey',
      'type': 'event'
    }]";
        public const string FillSurveyLog = @"[{
          'anonymous': false,
          'inputs': [
            {
              'indexed': false,
              'internalType': 'string',
              'name': 'docId',
              'type': 'string'
            },
            {
              'indexed': true,
              'internalType': 'address',
              'name': 'RewardFrom',
              'type': 'address'
            },
            {
              'indexed': true,
              'internalType': 'address',
              'name': 'RewardTo',
              'type': 'address'
            },
            {
              'indexed': false,
              'internalType': 'uint256',
              'name': 'Reward',
              'type': 'uint256'
            },
 {
              'indexed': false,
              'internalType': 'uint256',
              'name': 'OpenReward',
              'type': 'uint256'
            },
            {
              'indexed': false,
              'internalType': 'uint256',
              'name': 'fillCount',
              'type': 'uint256'
            }
          ],
          'name': 'FillSurvey',
          'type': 'event'
        }]";
        public const string LotteryLog = @"[ {
      'anonymous': false,
      'inputs': [
        {
          'indexed': false,
          'internalType': 'string',
          'name': 'docId',
          'type': 'string'
        },
        {
          'indexed': true,
          'internalType': 'address',
          'name': 'winnerAddress',
          'type': 'address'
        },
        {
          'indexed': false,
          'internalType': 'uint256',
          'name': 'reward',
          'type': 'uint256'
        }
      ],
      'name': 'LotteryLog',
      'type': 'event'
    }]";
        public const string CloseSurveyAbi = @"[{
      'inputs': [
        {
          'internalType': 'string',
          'name': 'docId',
          'type': 'string'
        }
      ],
      'name': 'closeSurvey',
      'outputs': [],
      'stateMutability': 'nonpayable',
      'type': 'function'
    }]";

    }
}
