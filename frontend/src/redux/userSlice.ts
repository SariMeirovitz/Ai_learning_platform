import { createSlice, type PayloadAction } from '@reduxjs/toolkit';
import { registerUserThunk, loginUserThunk } from './thunk'; // הוסף loginUserThunk

export interface User {
  id: string;
  name: string;
  phone: string;
}

interface UserState {
  user: User | null;
  loading: boolean;
  error: string | null;
  success: boolean;
}

const initialState: UserState = {
  user: null,
  loading: false,
  error: null,
  success: false,
};

const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {
    clearUser(state) {
      state.user = null;
      state.success = false;
      state.error = null;
    },
    clearStatus(state) {
      state.success = false;
      state.error = null;
    },
  },
  extraReducers: builder => {
    builder
      // register
      .addCase(registerUserThunk.pending, state => {
        state.loading = true;
        state.error = null;
        state.success = false;
      })
      .addCase(registerUserThunk.fulfilled, (state, action: PayloadAction<User>) => {
        state.loading = false;
        state.user = action.payload;
        state.success = true;
      })
      .addCase(registerUserThunk.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
        state.success = false;
      })
      // login
      .addCase(loginUserThunk.pending, state => {
        state.loading = true;
        state.error = null;
        state.success = false;
      })
      .addCase(loginUserThunk.fulfilled, (state, action: PayloadAction<User>) => {
        state.loading = false;
        state.user = action.payload;
        state.success = true;
      })
      .addCase(loginUserThunk.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
        state.success = false;
      });
  },
});

export const { clearUser, clearStatus } = userSlice.actions;
export default userSlice.reducer;