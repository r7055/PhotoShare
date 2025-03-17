import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import axios from 'axios';
import { UserLogin, UserRegister } from '../types/user';

const url = 'http://localhost:5141//api/auth';

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
    async (userData: UserRegister, thunkAPI) => {
        try {
            const response = await axios.post(`${url}/register`, userData);
            return response.data;
        } catch (e: any) {
            return thunkAPI.rejectWithValue(e.message);
        }
    }
);


const userSlice = createSlice({
    name: 'user',
    initialState: { 
        user: {} as UserRegister, 
        loading: true, 
        msg: '' // הוסף את השדה msg
    },
    reducers: {},
    extraReducers: (builder) => {
        builder
        .addCase(loginUser.fulfilled, (state, action) => {
            state.user = action.payload as UserRegister;
            state.loading = false;
            state.msg = ''; // נקה את המסר במקרה של הצלחה
        })
        .addCase(loginUser.rejected, (state, action) => {
            state.loading = false;
            state.msg = action.payload as string || "Login failed"; // עדכן את המסר במקרה של שגיאה
        })
        .addCase(loginUser.pending, (state) => {
            state.loading = true;
        })
        .addCase(registerUser.fulfilled, (state, action) => {
            state.user = action.payload as UserRegister;
            state.loading = false;
            state.msg = ''; // נקה את המסר במקרה של הצלחה
        })
        .addCase(registerUser.rejected, (state, action) => {
            state.loading = false;
            state.msg = action.payload as string || "Registration failed"; // עדכן את המסר במקרה של שגיאה
        })
        .addCase(registerUser.pending, (state) => {
            state.loading = true;
        });
    }
});

export default userSlice.reducer;