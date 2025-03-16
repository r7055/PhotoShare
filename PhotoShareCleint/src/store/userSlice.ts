// features/userSlice.js
import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios from "axios";
import Swal from "sweetalert2";
import { User } from "../types/user";

const url = 'http://localhost:3000/api/users';

// Async thunk for fetching user data
export const fetchUserData = createAsyncThunk('user/fetch',
    async (userId, thunkAPI) => {
        try {
            const response = await axios.get(`${url}/${userId}`);
            return response.data;
        } catch (e) {
            if (e instanceof Error) {
                return thunkAPI.rejectWithValue(e.message);
            }
            return thunkAPI.rejectWithValue('An unknown error occurred');
        }
    }
);

// Async thunk for logging in a user
export const loginUser = createAsyncThunk('user/login',
    async (userData, thunkAPI) => {
        try {
            const response = await axios.post(`${url}/login`, userData);
            return response.data;
        } catch (e) {
            return thunkAPI.rejectWithValue(e.message);
        }
    }
);

const userSlice = createSlice({
    name: 'user',
    initialState: { userInfo: User, loading: false },
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(fetchUserData.fulfilled, (state, action) => {
                state.userInfo = action.payload;
                state.loading = false;
            })
            .addCase(fetchUserData.rejected, () => {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Couldn't fetch user data",
                });
            })
            .addCase(loginUser.fulfilled, (state, action) => {
                state.userInfo = action.payload;
                state.loading = false;
                Swal.fire({
                    title: "Login Successful",
                    text: "Welcome back!",
                    icon: "success",
                });
            })
            .addCase(loginUser.rejected, () => {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Login failed",
                });
            });
    }
});

export default userSlice.reducer;
