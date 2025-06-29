import { createSlice } from '@reduxjs/toolkit';
import { fetchAllUsersWithPromptsThunk } from './thunk';

const adminSlice = createSlice({
  name: 'admin',
  initialState: {
    users: [],
    loading: false,
    error: null as string | null,
  },
  reducers: {},
  extraReducers: builder => {
    builder
      .addCase(fetchAllUsersWithPromptsThunk.pending, state => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchAllUsersWithPromptsThunk.fulfilled, (state, action) => {
        state.loading = false;
        state.users = action.payload;
      })
      .addCase(fetchAllUsersWithPromptsThunk.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      });
  },
});

export default adminSlice.reducer;