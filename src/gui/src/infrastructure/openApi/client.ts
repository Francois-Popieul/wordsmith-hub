import { z } from "zod";
import axios, { type AxiosInstance, type AxiosRequestConfig } from "axios";

const AppUserDto = z.object({
  id: z.string(),
  firstName: z.string().nullable(),
  lastName: z.string().nullable(),
  email: z.string(),
  userName: z.string(),
  phoneNumber: z.string().nullable(),
});
const Status = z.object({
  id: z.number().int(),
  name: z.string(),
  category: z.string(),
});
const Service = z.object({ id: z.number().int(), name: z.string() });
const RateDto = z.object({
  id: z.string(),
  unitPrice: z.number(),
  unit: z.string(),
  sourceLanguageId: z.number().int(),
  targetLanguageId: z.number().int(),
  serviceId: z.number().int(),
  directCustomerId: z.string(),
});
const AddRateRequest = z.object({
  unitPrice: z.number().gt(0),
  unit: z.string().min(0).max(20),
  sourceLanguageId: z.number().int().gt(0),
  targetLanguageId: z.number().int().gt(0),
  serviceId: z.number().int().gt(0),
  directCustomerId: z.string().optional(),
});
const AddProjectRequest = z.object({
  name: z.string().min(0).max(150),
  domain: z.string().min(0).max(100),
  description: z.string().min(0).max(1000).nullish(),
  directCustomerIds: z.array(z.string()),
  endCustomerName: z.string().nullish(),
});
const UpdateProjectStatusRequest = z.object({
  statusId: z.number().int().gte(30).lte(31),
});
const EndCustomerDto = z.object({
  id: z.string(),
  name: z.string(),
  statusId: z.number().int(),
});
const AddressDto = z.object({
  streetInfo: z.string(),
  addressComplement: z.string().nullable(),
  postCode: z.string(),
  city: z.string(),
  state: z.string().nullable(),
  countryId: z.number().int(),
});
const DirectCustomerDto = z.object({
  id: z.string(),
  name: z.string(),
  code: z.string(),
  phone: z.string().nullable(),
  email: z.string(),
  address: AddressDto,
  siretOrSiren: z.string().nullable(),
  paymentDelay: z.number().int(),
  currencyId: z.number().int(),
  statusId: z.number().int(),
});
const ProjectDto = z.object({
  id: z.string(),
  name: z.string(),
  domain: z.string(),
  description: z.string().nullable(),
  endCustomer: EndCustomerDto.nullable(),
  directCustomers: z.array(DirectCustomerDto),
  statusId: z.number().int(),
});
const LegalStatusType = z.object({
  id: z.number().int(),
  name: z.string(),
  code: z.string(),
});
const UpdateLegalStatusRequest = z.object({
  legalStatusId: z.string().min(1),
  legalStatusTypeId: z.number().int().optional(),
  siret: z.string().nullish(),
  vatNumber: z.string().nullish(),
  vatExemption: z.boolean().optional(),
  vatRate: z.number().nullish(),
  taxDeductionExemption: z.boolean().optional(),
  validFrom: z.string().datetime({ offset: true }).optional(),
  validTo: z.string().datetime({ offset: true }).nullish(),
});
const AddLegalStatusRequest = z.object({
  legalStatusTypeId: z.number().int(),
  siret: z.string().min(0).max(14).nullish(),
  vatNumber: z.string().min(0).max(13).nullish(),
  vatExemption: z.boolean().optional(),
  vatRate: z.number().gte(0).nullish(),
  taxDeductionExemption: z.boolean().optional(),
  validFrom: z.string().datetime({ offset: true }).optional(),
  validTo: z.string().datetime({ offset: true }).nullish(),
});
const LegalStatusDto = z.object({
  id: z.string(),
  legalStatusType: LegalStatusType.nullable(),
  siret: z.string().nullable(),
  vatNumber: z.string().nullable(),
  vatExemption: z.boolean(),
  vatRate: z.number().nullable(),
  taxDeductionExemption: z.boolean(),
  validFrom: z.string().datetime({ offset: true }),
  validTo: z.string().datetime({ offset: true }).nullable(),
});
const TranslationLanguage = z.object({
  id: z.number().int(),
  name: z.string(),
  code: z.string(),
});
const Address = z.object({
  streetInfo: z.string().min(0).max(255),
  addressComplement: z.string().min(0).max(255).nullable(),
  postCode: z.string().min(0).max(10),
  city: z.string().min(0).max(100),
  state: z.string().min(0).max(50).nullable(),
  countryId: z.number().int(),
});
const UpdateFreelanceAddressRequest = z.object({ address: Address });
const UpdateFreelanceRequest = z.object({
  firstName: z.string().min(0).max(50),
  lastName: z.string().min(0).max(100),
  email: z
    .string()
    .min(0)
    .max(255)
    .regex(/^[^@]+@[^@]+$/)
    .email(),
  phone: z.string().min(0).max(20).nullish(),
  address: Address,
});
const UpdateFreelanceLanguagesRequest = z.object({
  sourceLanguageIds: z.array(z.number().int()),
  targetLanguageIds: z.array(z.number().int()),
});
const UpdateFreelancePersonalDataRequest = z.object({
  firstName: z.string().min(0).max(50),
  lastName: z.string().min(0).max(100),
  email: z
    .string()
    .min(0)
    .max(255)
    .regex(/^[^@]+@[^@]+$/)
    .email(),
  phone: z.string().min(0).max(20).nullish(),
});
const UpdateFreelanceServicesRequest = z.object({
  serviceIds: z.array(z.number().int()),
});
const ProfileDto = z.object({
  id: z.string(),
  firstName: z.string(),
  lastName: z.string(),
  phone: z.string().nullable(),
  email: z.string(),
  address: AddressDto.nullable(),
  statusId: z.number().int(),
  sourceLanguages: z.array(TranslationLanguage),
  targetLanguages: z.array(TranslationLanguage),
  services: z.array(Service),
});
const FreelanceDto = z.object({
  id: z.string(),
  firstName: z.string(),
  lastName: z.string(),
  phone: z.string().nullable(),
  email: z.string(),
  address: AddressDto.nullable(),
  statusId: z.number().int(),
});
const UpdateDirectCustomerRequest = z.object({
  name: z.string().min(0).max(150),
  code: z.string().min(0).max(5),
  phone: z.string().min(0).max(20).nullish(),
  email: z
    .string()
    .min(0)
    .max(255)
    .regex(/^[^@]+@[^@]+$/)
    .email(),
  address: Address,
  siretOrSiren: z.string().min(0).max(15).nullish(),
  paymentDelay: z.number().int(),
  currencyId: z.number().int(),
});
const AddDirectCustomerRequest = z.object({
  name: z.string().min(0).max(150),
  code: z.string().min(0).max(5),
  phone: z.string().min(0).max(20).nullish(),
  email: z
    .string()
    .min(0)
    .max(255)
    .regex(/^[^@]+@[^@]+$/)
    .email(),
  address: Address,
  siretOrSiren: z.string().min(0).max(15).nullish(),
  paymentDelay: z.number().int(),
  currencyId: z.number().int(),
});
const Currency = z.object({
  id: z.number().int(),
  name: z.string(),
  code: z.string(),
  symbol: z.string(),
});
const Country = z.object({
  id: z.number().int(),
  name: z.string(),
  code: z.string(),
  isEuropeanUnionMember: z.boolean(),
});
const UpdateBankAccountRequest = z.object({
  bankAccountId: z.string().min(1),
  label: z.string().min(0).max(100),
  bankName: z.string().min(0).max(100),
  accountHolderName: z.string().min(0).max(100),
  iban: z.string().min(0).max(34),
  bic: z.string().min(0).max(11),
  isDefault: z.boolean(),
});
const AddBankAccountRequest = z.object({
  label: z.string(),
  bankName: z.string(),
  accountHolderName: z.string(),
  iban: z.string(),
  bic: z.string(),
});
const BankAccountDto = z.object({
  id: z.string(),
  label: z.string(),
  bankName: z.string(),
  accountHolderName: z.string(),
  iban: z.string(),
  bic: z.string(),
  isDefault: z.boolean(),
});
const LoginUserRequest = z.object({
  email: z
    .string()
    .min(0)
    .max(255)
    .regex(/^[^@]+@[^@]+$/)
    .email(),
  password: z.string().min(0).max(255),
});
const AccessTokenResponse = z.object({
  tokenType: z.string(),
  accessToken: z.string(),
  expiresIn: z.number().int(),
  refreshToken: z.string(),
});
const RegisterUserRequest = z.object({
  firstName: z.string().min(0).max(50).nullish(),
  lastName: z.string().min(0).max(100).nullish(),
  email: z
    .string()
    .min(0)
    .max(255)
    .regex(/^[^@]+@[^@]+$/)
    .email(),
  password: z.string().min(12).max(255),
  passwordConfirmation: z.string().nullish(),
});

export const schemas = {
  AppUserDto,
  Status,
  Service,
  RateDto,
  AddRateRequest,
  AddProjectRequest,
  UpdateProjectStatusRequest,
  EndCustomerDto,
  AddressDto,
  DirectCustomerDto,
  ProjectDto,
  LegalStatusType,
  UpdateLegalStatusRequest,
  AddLegalStatusRequest,
  LegalStatusDto,
  TranslationLanguage,
  Address,
  UpdateFreelanceAddressRequest,
  UpdateFreelanceRequest,
  UpdateFreelanceLanguagesRequest,
  UpdateFreelancePersonalDataRequest,
  UpdateFreelanceServicesRequest,
  ProfileDto,
  FreelanceDto,
  UpdateDirectCustomerRequest,
  AddDirectCustomerRequest,
  Currency,
  Country,
  UpdateBankAccountRequest,
  AddBankAccountRequest,
  BankAccountDto,
  LoginUserRequest,
  AccessTokenResponse,
  RegisterUserRequest,
};

function buildUrl(
  path: string,
  pathParams?: Record<string, string | number>
): string {
  if (!pathParams) return path;
  return Object.entries(pathParams).reduce(
    (url, [key, value]) =>
      url.replace(`:${key}`, encodeURIComponent(String(value))),
    path
  );
}

export type ApiClientOptions = {
  axiosConfig?: AxiosRequestConfig;
};

export function createApiClient(baseUrl: string, options?: ApiClientOptions) {
  const instance: AxiosInstance = axios.create({
    baseURL: baseUrl,
    ...options?.axiosConfig,
  });

  async function request<T>(
    method: string,
    path: string,
    params: {
      body?: unknown;
      pathParams?: Record<string, string | number>;
      query?: Record<string, unknown>;
    },
    responseSchema: z.ZodType<T>,
    config?: AxiosRequestConfig
  ): Promise<T> {
    const url = buildUrl(path, params.pathParams);
    const response = await instance.request({
      method,
      url,
      data: params.body,
      params: params.query,
      ...config,
    });

    // Axios can return an empty string for 204 responses in some environments.
    // Normalize no-content payloads so z.void() schemas parse reliably.
    const canBeVoid = responseSchema.safeParse(undefined).success;
    const responseData =
      response.status === 204 || (canBeVoid && response.data === "")
        ? undefined
        : response.data;

    return responseSchema.parse(responseData);
  }

  return {
    instance,
    LoginUserEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) => request("post", "/auth/login", params, AccessTokenResponse, config),
    RegisterUserEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) => request("post", "/auth/register", params, z.string(), config),
    UpdateBankAccountEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) => request("put", "/bankaccount", params, z.string(), config),
    AddBankAccountEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) => request("post", "/bankaccount", params, z.string(), config),
    UpdateDefaultBankAccountEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) =>
      request("put", "/bankaccount/:bankAccountId", params, z.string(), config),
    DeleteBankAccountEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) =>
      request(
        "delete",
        "/bankaccount/:bankAccountId",
        params,
        z.void(),
        config
      ),
    GetAllBankAccountsEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) =>
      request("get", "/bankaccounts", params, z.array(BankAccountDto), config),
    GetAllCountriesEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) => request("get", "/countries", params, z.array(Country), config),
    GetAllCurrenciesEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) => request("get", "/currencies", params, z.array(Currency), config),
    AddDirectCustomerEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) => request("post", "/directcustomer", params, z.string(), config),
    UpdateDirectCustomerEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) =>
      request(
        "put",
        "/directcustomer/:directCustomerId",
        params,
        z.string(),
        config
      ),
    GetDirectCustomerEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) =>
      request(
        "get",
        "/directcustomer/:directCustomerId",
        params,
        DirectCustomerDto,
        config
      ),
    DeleteDirectCustomerEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) =>
      request(
        "delete",
        "/directcustomer/:directCustomerId",
        params,
        z.void(),
        config
      ),
    GetAllDirectCustomersEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) =>
      request(
        "get",
        "/directcustomers",
        params,
        z.array(DirectCustomerDto),
        config
      ),
    GetFreelanceEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) => request("get", "/freelance", params, ProfileDto, config),
    UpdateFreelanceEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) => request("put", "/freelance/:freelanceId", params, z.string(), config),
    DeleteFreelanceEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) => request("delete", "/freelance/:freelanceId", params, z.void(), config),
    UpdateFreelanceAddressEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) =>
      request(
        "put",
        "/freelance/:freelanceId/address",
        params,
        z.string(),
        config
      ),
    UpdateFreelanceLanguagesEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) =>
      request(
        "put",
        "/freelance/:freelanceId/languages",
        params,
        z.string(),
        config
      ),
    UpdateFreelancePersonalDataEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) =>
      request(
        "put",
        "/freelance/:freelanceId/personaldata",
        params,
        z.string(),
        config
      ),
    UpdateFreelanceServicesEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) =>
      request(
        "put",
        "/freelance/:freelanceId/services",
        params,
        z.string(),
        config
      ),
    GetAllFreelancesEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) => request("get", "/freelances", params, z.array(FreelanceDto), config),
    GetAllInvoiceStatusesEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) => request("get", "/invoice-statuses", params, z.array(Status), config),
    GetAllLanguagesEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) =>
      request(
        "get",
        "/languages",
        params,
        z.array(TranslationLanguage),
        config
      ),
    GetAllLegalStatusTypesEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) =>
      request(
        "get",
        "/legal-status-types",
        params,
        z.array(LegalStatusType),
        config
      ),
    UpdateLegalStatusEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) => request("put", "/legalstatus", params, z.string(), config),
    AddLegalStatusEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) => request("post", "/legalstatus", params, z.string(), config),
    DeleteLegalStatusEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) =>
      request(
        "delete",
        "/legalstatus/:legalStatusId",
        params,
        z.void(),
        config
      ),
    GetAllLegalStatusesEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) =>
      request("get", "/legalstatuses", params, z.array(LegalStatusDto), config),
    AddProjectEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) => request("post", "/project", params, z.string(), config),
    GetAllProjectStatusesEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) => request("get", "/project-statuses", params, z.array(Status), config),
    UpdateProjectEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) => request("put", "/project/:projectId", params, z.string(), config),
    DeleteProjectEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) => request("delete", "/project/:projectId", params, z.void(), config),
    UpdateProjectStatusEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) =>
      request("put", "/project/:projectId/status", params, z.string(), config),
    GetAllProjectsEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) => request("get", "/projects", params, z.array(ProjectDto), config),
    GetAllProjectsByDirectCustomerEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) =>
      request(
        "get",
        "/projects/directcustomer/:directCustomerId",
        params,
        z.array(ProjectDto),
        config
      ),
    AddRateEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) => request("post", "/rate", params, z.string(), config),
    DeleteRateEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) => request("delete", "/rate/:rateId", params, z.void(), config),
    GetAllRatesEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) => request("get", "/rates", params, z.array(RateDto), config),
    GetAllRatesByCustomerIdEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) =>
      request(
        "get",
        "/rates/directcustomer/:directCustomerId",
        params,
        z.array(RateDto),
        config
      ),
    GetAllServicesEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) => request("get", "/services", params, z.array(Service), config),
    GetUserEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) => request("get", "/user/:userId", params, AppUserDto, config),
    GetAllWorkOrderStatusesEndpoint: (
      params: {
        body?: unknown;
        pathParams?: Record<string, string | number>;
        query?: Record<string, unknown>;
      } = {},
      config?: AxiosRequestConfig
    ) => request("get", "/workorder-statuses", params, z.array(Status), config),
  };
}

// Helper function to get the first tag from an endpoint by alias
export function getTagByAlias(alias: string): string | undefined {
  const endpointMap: Record<string, string | undefined> = {
    LoginUserEndpoint: "authentication",
    RegisterUserEndpoint: "authentication",
    UpdateBankAccountEndpoint: "bankaccount",
    AddBankAccountEndpoint: "bankaccount",
    UpdateDefaultBankAccountEndpoint: "bankaccount",
    DeleteBankAccountEndpoint: "bankaccount",
    GetAllBankAccountsEndpoint: "bankaccount",
    GetAllCountriesEndpoint: "countries",
    GetAllCurrenciesEndpoint: "currencies",
    AddDirectCustomerEndpoint: "directcustomer",
    UpdateDirectCustomerEndpoint: "directcustomer",
    GetDirectCustomerEndpoint: "directcustomer",
    DeleteDirectCustomerEndpoint: "directcustomer",
    GetAllDirectCustomersEndpoint: "directcustomer",
    GetFreelanceEndpoint: "freelance",
    UpdateFreelanceEndpoint: "freelance",
    DeleteFreelanceEndpoint: "freelance",
    UpdateFreelanceAddressEndpoint: "freelance",
    UpdateFreelanceLanguagesEndpoint: "freelance",
    UpdateFreelancePersonalDataEndpoint: "freelance",
    UpdateFreelanceServicesEndpoint: "freelance",
    GetAllFreelancesEndpoint: "freelance",
    GetAllInvoiceStatusesEndpoint: "statuses",
    GetAllLanguagesEndpoint: "languages",
    GetAllLegalStatusTypesEndpoint: "legal-status-types",
    UpdateLegalStatusEndpoint: "legalstatus",
    AddLegalStatusEndpoint: "legalstatus",
    DeleteLegalStatusEndpoint: "legalstatus",
    GetAllLegalStatusesEndpoint: "legalstatus",
    AddProjectEndpoint: "project",
    GetAllProjectStatusesEndpoint: "statuses",
    UpdateProjectEndpoint: "project",
    DeleteProjectEndpoint: "project",
    UpdateProjectStatusEndpoint: "project",
    GetAllProjectsEndpoint: "projects",
    GetAllProjectsByDirectCustomerEndpoint: "projects",
    AddRateEndpoint: "rate",
    DeleteRateEndpoint: "rate",
    GetAllRatesEndpoint: "rates",
    GetAllRatesByCustomerIdEndpoint: "rates",
    GetAllServicesEndpoint: "services",
    GetUserEndpoint: "user",
    GetAllWorkOrderStatusesEndpoint: "statuses",
  };
  return endpointMap[alias];
}
