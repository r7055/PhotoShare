import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import axios from 'axios';
import Swal from 'sweetalert2';
import { UserLogin } from '../types/user';

const url = 'http://localhost:3000/api/auth';

// Async thunk for logging in a user
export const loginUser = createAsyncThunk('user/login',
    async (userData: UserLogin, thunkAPI) => {
        try {
            const response = await axios.post(`${url}/login`, userData);
            return response.data;
        } catch (e: any) {  
            return thunkAPI.rejectWithValue(e.message);
        }
    }
);

// Async thunk for registering a user
export const registerUser = createAsyncThunk('user/register',
    async (userData: UserLogin, thunkAPI) => {
        try {
            const response = await axios.post(`${url}/register`, userData);
            return response.data;
        } catch (e: any) {
            return thunkAPI.rejectWithValue(e.message);
        }
    }
);

interface UserState {
    userInfo: UserLogin | null;
    loading: boolean;
}

const initialState: UserState = { userInfo: null, loading: false };

const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(loginUser.fulfilled, (state, action) => {
                state.userInfo = action.payload;
                state.loading = false;
                Swal.fire({
                    title: "Login Successful",
                    text: "Welcome back!",
                    icon: "success",
                });
            })
            .addCase(loginUser.rejected, (state, action) => {
                state.loading = false;
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: action.payload as string || "Login failed",
                });
            })
            .addCase(loginUser.pending, (state) => {
                state.loading = true;
            })
            .addCase(registerUser.fulfilled, (state, action) => {
                state.userInfo = action.payload;
                state.loading = false;
                Swal.fire({
                    title: "Registration Successful",
                    text: "You can now log in!",
                    icon: "success",
                });
            })
            .addCase(registerUser.rejected, (state, action) => {
                state.loading = false;
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: action.payload as string || "Registration failed",
                });
            })
            .addCase(registerUser.pending, (state) => {
                state.loading = true;
            });
    }
});

export default userSlice.reducer;