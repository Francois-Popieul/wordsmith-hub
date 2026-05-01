import { StrictMode } from "react"
import { createRoot } from "react-dom/client"
import HomepageView from "./homepage/views/HomepageView.tsx"
import SignupView from "./authentication/views/SignupView.tsx"
import LoginView from "./authentication/views/LoginView.tsx"
import DashboardView from "./dashboard/views/DashboardView.tsx"
import DirectCustomers from "./directCustomers/views/directCustomers.tsx"
import OrderView from "./orders/OrderView.tsx"
import ProfileView from "./profile/ProfileView.tsx"
import { BrowserRouter, Route, Routes } from "react-router";
import TermsOfService from "./terms_of_service/views/TermsOfService.tsx"
import PrivacyPolicy from "./privacy_policy/views/PrivacyPolicy.tsx"
import Invoices from "./invoices/views/Invoices.tsx"

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<HomepageView />} />
        <Route path="/signup" element={<SignupView />} />
        <Route path="/login" element={<LoginView />} />
        <Route path="/dashboard" element={<DashboardView />} />
        <Route path="/direct-customers" element={<DirectCustomers />} />
        <Route path="/invoices" element={<Invoices />} />
        <Route path="/orders" element={<OrderView />} />
        <Route path="/profile" element={<ProfileView />} />
        <Route path="/terms_of_service" element={<TermsOfService />} />
        <Route path="/privacy_policy" element={<PrivacyPolicy />} />
      </Routes>
    </BrowserRouter>
  </StrictMode>,
)
