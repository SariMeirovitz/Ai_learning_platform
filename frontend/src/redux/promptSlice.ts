import { createSlice } from '@reduxjs/toolkit';
import { fetchMyHistoryThunk, sendPromptThunk } from './thunk';

interface Prompt {
  id: number;
  prompt: string;
  response: string;
  categoryId: number;
  subCategoryId: number;
  createdAt?: string;
}

interface PromptState {
  loading: boolean;
  error: string | null;
  response: string | null;
  prompts: Prompt[];
}

const initialState: PromptState = {
  loading: false,
  error: null,
  response: null,
  prompts: [],
};

const promptSlice = createSlice({
  name: 'prompt',
  initialState,
  reducers: {
    clearPromptState(state) {
      state.loading = false;
      state.error = null;
      state.response = null;
    },
  },
  extraReducers: builder => {
    builder
      // שליחת prompt
      .addCase(sendPromptThunk.pending, state => {
        state.loading = true;
        state.error = null;
        state.response = null;
      })
      .addCase(sendPromptThunk.fulfilled, (state, action) => {
        state.loading = false;
        // אם השרת מחזיר אובייקט עם שדה response
        state.response = action.payload?.response || '';
      })
      .addCase(sendPromptThunk.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      // שליפת היסטוריית למידה
      .addCase(fetchMyHistoryThunk.pending, state => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchMyHistoryThunk.fulfilled, (state, action) => {
        state.loading = false;
        state.prompts = action.payload || [];
      })
      .addCase(fetchMyHistoryThunk.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      });
  },
});

export const { clearPromptState } = promptSlice.actions;
export default promptSlice.reducer;