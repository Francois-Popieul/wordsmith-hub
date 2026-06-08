import { StrictMode } from "react"
import { createRoot } from "react-dom/client"
import HomepageView from "./homepage/views/HomepageView.tsx"
import SignupView from "./authentication/views/SignupView.tsx"
import LoginView from "./authentication/views/LoginView.tsx"
import DashboardView from "./dashboard/views/DashboardView.tsx"
import OrdersView from "./orders/OrdersView.tsx"
import ProfileView from "./profile/views/ProfileView.tsx"
import { BrowserRouter, Outlet, Route, Routes } from "react-router-dom";
import TermsOfService from "./terms_of_service/views/TermsOfService.tsx"
import PrivacyPolicy from "./privacy_policy/views/PrivacyPolicy.tsx"
import InvoicesView from "./invoices/views/InvoicesView.tsx"
import ProjectsView from "./projects/views/ProjectsView.tsx"
import DirectCustomersView from "./directCustomers/views/DirectCustomersView.tsx"
import DirectCustomerView from "./directCustomers/views/DirectCustomerView.tsx"
import { ToastProvider } from "./hooks/useToast/ToastProvider.tsx"
import { StaticDataProvider } from "./hooks/useStaticTables/StaticDataProvider.tsx"
import { AuthProvider } from "./hooks/useAuth/AuthProvider.tsx"
import ProtectedRoute from "./components/partials/ProtectedRoute.tsx"
import ForgotPasswordView from "./authentication/views/ForgotPasswordView.tsx"
import Faq from "./faq/views/Faq.tsx"

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <AuthProvider>
      <ToastProvider>
        <BrowserRouter>
          <Routes>
            <Route path="/" element={<HomepageView />} />
            <Route path="/signup" element={<SignupView />} />
            <Route path="/login" element={<LoginView />} />
            <Route path="/forgot-password" element={<ForgotPasswordView />} />
            <Route element={<StaticDataProvider><ProtectedRoute><Outlet /></ProtectedRoute></StaticDataProvider>}>
              <Route path="/dashboard" element={<DashboardView />} />
              <Route path="/direct-customers" element={<DirectCustomersView />} />
              <Route path="/direct-customer/:id" element={<DirectCustomerView />} />
              <Route path="/projects" element={<ProjectsView />} />
              <Route path="/orders" element={<OrdersView />} />
              <Route path="/invoices" element={<InvoicesView />} />
              <Route path="/profile" element={<ProfileView />} />
            </Route>
            <Route path="/terms_of_service" element={<TermsOfService />} />
            <Route path="/privacy_policy" element={<PrivacyPolicy />} />
            <Route path="/faq" element={<Faq />} />
          </Routes>
        </BrowserRouter>
      </ToastProvider>
    </AuthProvider>
  </StrictMode>,
)
