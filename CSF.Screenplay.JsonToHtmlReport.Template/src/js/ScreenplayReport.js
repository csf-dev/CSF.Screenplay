// @flow

/**
 * This type and those which are used in its object graph should be kept in-sync with the C# types of the same names.
 * Those are found in the `CSF.Screenplay` project, in the directory `ReportModel`.
 */
export type ScreenplayReport = {
    Metadata : ReportMetadata,
    Performances : PerformanceReport[]
}

export type ReportMetadata = {
    Timestamp : string,
    ReportVersion : string,
}

export type PerformanceReport = {
    NamingHierarchy : IdentifierAndNameModel[],
    Outcome : string,
    Reportables : ReportableModelBase[],
    Started : string
}

export type IdentifierAndNameModel = {
    Id : string | null,
    Name : string,
    IsGeneratedId : boolean
}

export const
    actorCreatedReportKind = 'ActorCreatedReport',
    actorGainedAbilityReportKind = 'ActorGainedAbilityReport',
    actorSpotlitReportKind = 'ActorSpotlitReport',
    spotlightTurnedOffReportKind = 'SpotlightTurnedOffReport',
    performableReportKind = 'PerformableReport';

type ReportableModelBase = {
    Report : string,
    Actor : string,
    Started : string,
}

export type ActorCreatedReport = {
    ...ReportableModelBase,
    Kind : typeof actorCreatedReportKind
}

export type ActorGainedAbilityReport = {
    ...ReportableModelBase,
    Kind : typeof actorGainedAbilityReportKind
}

export type ActorSpotlitReport = {
    ...ReportableModelBase,
    Kind : typeof actorSpotlitReportKind
}

export type SpotlightTurnedOffReport = {
    ...ReportableModelBase,
    Kind : typeof spotlightTurnedOffReportKind
}

export type PerformableReport = {
    ...ReportableModelBase,
    Kind : typeof performableReportKind,
    Type : string,
    Phase : string,
    Ended : string,
    Result : string,
    HasResult : boolean,
    Exception : string,
    ExceptionIsBubbling : boolean,
    Assets : PerformableAsset[],
    Reportables: ReportableModel[]
}

export type PerformableAsset = {
    ContentType : string,
    Path : string,
    Data : string,
    Name : string,
    Summary : string
}

export type ReportableModel =
    | ActorCreatedReport
    | ActorGainedAbilityReport
    | ActorSpotlitReport
    | SpotlightTurnedOffReport
    | PerformableReport;
    
export type ReportableKind = ReportableModel['Kind'];
