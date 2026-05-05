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
const Service = z.object({ id: z.number().int(), name: z.string() });
const TranslationLanguage = z.object({
  id: z.number().int(),
  name: z.string(),
  code: z.string(),
});
const Address = z.object({
  streetInfo: z.string(),
  addressComplement: z.string().nullable(),
  postCode: z.string(),
  city: z.string(),
  state: z.string().nullable(),
  countryId: z.number().int(),
});
const UpdateFreelanceRequest = z.object({
  firstName: z.string().min(0).max(50),
  lastName: z.string().min(0).max(100),
  email: z
    .string()
    .min(0)
    .max(255)
    .regex(/^[^@]+@[^@]+$/)
    .email(),
  phone: z.string().min(0).max(15).nullish(),
  address: Address,
});
const NoContent = z.object({});
const AddressDto = z.object({
  streetInfo: z.string(),
  addressComplement: z.string().nullable(),
  postCode: z.string(),
  city: z.string(),
  state: z.string().nullable(),
  countryId: z.number().int(),
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
  phone: z.string().min(0).max(15).nullish(),
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
const AddDirectCustomerRequest = z.object({
  name: z.string().min(0).max(150),
  code: z.string().min(0).max(5),
  phone: z.string().min(0).max(15).nullish(),
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
  Service,
  TranslationLanguage,
  Address,
  UpdateFreelanceRequest,
  NoContent,
  AddressDto,
  ProfileDto,
  FreelanceDto,
  UpdateDirectCustomerRequest,
  DirectCustomerDto,
  AddDirectCustomerRequest,
  Currency,
  Country,
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
    return responseSchema.parse(response.data);
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
        z.object({}),
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
    ) =>
      request(
        "delete",
        "/freelance/:freelanceId",
        params,
        z.object({}),
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
  };
}

// Helper function to get the first tag from an endpoint by alias
export function getTagByAlias(alias: string): string | undefined {
  const endpointMap: Record<string, string | undefined> = {
    LoginUserEndpoint: "authentication",
    RegisterUserEndpoint: "authentication",
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
    GetAllFreelancesEndpoint: "freelance",
    GetAllLanguagesEndpoint: "languages",
    GetAllServicesEndpoint: "services",
    GetUserEndpoint: "user",
  };
  return endpointMap[alias];
}
