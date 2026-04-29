import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import Homepage from './views/homepage.tsx'
import Signup from './authentication/views/signup.tsx'
import Login from './authentication/views/login.tsx'
import Dashboard from './views/dashboard.tsx'
import DirectCustomers from './views/directCustomers.tsx'
import Orders from './views/orders.tsx'
import Profile from './views/profile.tsx'
import { BrowserRouter, Route, Routes } from "react-router";

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Homepage />} />
        <Route path="/signup" element={<Signup />} />
        <Route path="/login" element={<Login />} />
        <Route path="/dashboard" element={<Dashboard />} />
        <Route path="/direct-customers" element={<DirectCustomers />} />
        <Route path="/orders" element={<Orders />} />
        <Route path="/profile" element={<Profile />} />
      </Routes>
    </BrowserRouter>
  </StrictMode>,
)
