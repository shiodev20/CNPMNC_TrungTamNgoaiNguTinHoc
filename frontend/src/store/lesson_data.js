import axios from "axios";
const leson = {
  namespaced: true,
  state: {
    lesson: null,
    data_post: {
      id: null,
      start_date: null,
      end_date: null,
      shift: null,
      start_hour: null,
      end_hour: null,
      class_id: null,
    },
    err: {
      id: null,
      start_date: null,
      end_date: null,
      shift: null,
      start_hour: null,
      end_hour: null,
      class_id: null,
    },
  },
  mutations: {
    set_value: (state, value) => {
      state.lesson = value;
    },
  },
  getters: {
    getbyid: (state) => (id) => {
      return state.lesson.filter((e) => e.id == id);
    },
  },
  actions: {
    async get_all({ commit }) {
      const data = await axios.get("http://localhost:3000/lesson");
      commit("set_value", data.data);
    },
    async create({ commit, state }) {
      console.log(commit);
      await axios.post("http://localhost:3000/lesson", state.data_post);
    },
    async update({ commit, state }) {
      console.log(commit);
      await axios.put(
        "http://localhost:3000/lesson/" + state.data_post.id,
        state.data_post
      );
      const data = await axios.get("http://localhost:3000/lesson");
      commit("set_value", data.data);
    },
    async delete({ commit }, id) {
      await axios.delete("http://localhost:3000/lesson/" + id);
      const data = await axios.get("http://localhost:3000/lesson");
      commit("set_value", data.data);
    },
  },
};
export default leson;
