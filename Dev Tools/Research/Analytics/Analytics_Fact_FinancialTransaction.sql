SELECT CONCAT (
        ft.Id
        ,'_'
        ,ftd.Id
        ) [TransactionKey]
    ,convert(INT, (convert(CHAR(8), ft.TransactionDateTime, 112))) [TransactionDateKey]
    ,ft.TransactionDateTime
    ,ft.TransactionCode [TransactionCode]
    ,ft.Summary [TransactionSummary]
    ,ft.TransactionTypeValueId [TransactionTypeValueId]
    ,ft.SourceTypeValueId [SourceTypeValueId]
    ,CASE ft.ScheduledTransactionId
        WHEN NULL
            THEN 0
        ELSE 1
        END [IsScheduled]
    ,
    -- todo [AuthorizedPersonKey]
    paAuthorizedPerson.PersonId [AuthorizedCurrentPersonId]
    ,
    -- todo ProcessedPersonKey
    paProcessedByPerson.PersonId [ProcessedCurrentPersonId]
    ,ft.ProcessedDateTime
    ,
    -- todo [GivingUnitKey]
    p.GivingGroupId [GivingGroupId]
    ,
    -- todo [BatchKey]
    ft.BatchId
    ,ft.FinancialGatewayId
    ,et.FriendlyName [EntityTypeName]
    ,ftd.EntityTypeId
    ,ftd.EntityId
    ,ft.Id [TransactionId]
    ,ftd.Id [TransactionDetailId]
    ,ftd.AccountId [AccountKey]
    ,fpd.CurrencyTypeValueId
    ,fpd.CreditCardTypeValueId
    ,
    -- TODO DaysSinceLastTransactionOfType, NumberOfDays (NULLable), Last Time This Giving Unit did a "Contribution/Event" TransactionType that is the same as this TransactionType
    -- TODO IsFirstTransactionOfType
    -- TODO AuthorizedFamilyKey
    -- TODO AuthorizedFamilyId
    1 [Count]
    ,ftd.Amount [Amount]
    ,ftd.ModifiedDateTime
FROM FinancialTransaction ft
JOIN FinancialTransactionDetail ftd ON ftd.TransactionId = ft.Id
JOIN PersonAlias paAuthorizedPerson ON ft.AuthorizedPersonAliasId = paAuthorizedPerson.Id
JOIN Person p ON p.Id = paAuthorizedPerson.PersonId
LEFT JOIN PersonAlias paProcessedByPerson ON ft.ProcessedByPersonAliasId = paProcessedByPerson.Id
LEFT JOIN EntityType et ON ftd.EntityTypeId = et.Id
LEFT JOIN FinancialPaymentDetail fpd ON ft.FinancialPaymentDetailId = fpd.Id
