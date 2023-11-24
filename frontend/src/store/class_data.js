import axios from "axios";
const classs = {
  namespaced: true,
  state: {
    class: null,
    data_post: {
      id: null,
      name: null,
      start_date: null,
      end_date: null,
      status: true,
      course_id: null,
      branch_id: null,
    },
    err: {
      name: null,
      start_date: null,
      end_date: null,
      branch: null,
      course: null,
    },
  },
  mutations: {
    set_value: (state, value) => {
      state.class = value;
    },
  },
  getters: {
    getbyid: (state) => (id) => {
      if (state.class == null) {
        return [];
      }
      return state.class.filter((e) => e.id == id);
    },
    getbyidcourse: (state) => (id) => {
      return state.class.filter((e) => e.course_id == id);
    },
  },
  actions: {
    async get_all({ commit }) {
      const data = await axios.get("http://localhost:3000/class");
      commit("set_value", data.data);
    },
    async create({ commit, state }) {
      console.log(commit);
      await axios.post("http://localhost:3000/class", state.data_post);
    },
    async update({ commit, state }) {
      console.log(commit);
      await axios.put(
        "http://localhost:3000/class/" + state.data_post.id,
        state.data_post
      );
    },
    async delete(context, id) {
      await axios.delete("http://localhost:3000/class/" + id);
      const data = await axios.get("http://localhost:3000/class");
      context.commit("set_value", data.data);
    },
  },
};
export default classs;
