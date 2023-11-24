import axios from "axios";
const course = {
  namespaced: true,
  state: {
    course: null,
    data_post: {
      id: null,
      id_lang: null,
      id_cer: null,
      name: null,
      price: null,
      decription: null,
    },
    err: {
      name: null,
      languages: null,
      certificate: null,
      price: null,
    },
  },
  mutations: {
    set_value: (state, value) => {
      state.course = value;
    },
  },
  getters: {
    getbyid: (state) => (id) => {
      if (state.course == null) {
        return [];
      }
      return state.course.filter((e) => e.id == id);
    },
  },
  actions: {
    async get_all({ commit }) {
      const data = await axios.get("http://localhost:3000/course");
      commit("set_value", data.data);
    },
    async create({ commit, state }) {
      console.log(commit);
      await axios.post("http://localhost:3000/course", state.data_post);
    },
    async update({ commit, state }) {
      console.log(commit);
      await axios.put(
        "http://localhost:3000/course/" + state.data_post.id,
        state.data_post
      );
    },
    async delete(context, id) {
      await axios.delete("http://localhost:3000/course/" + id);
      const data = await axios.get("http://localhost:3000/course");
      context.commit("set_value", data.data);
    },
  },
};
export default course;
