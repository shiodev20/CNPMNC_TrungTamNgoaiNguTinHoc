import axios from "axios";
const acounts = {
  namespaced: true,
  state: {
    content_exame: null,
    data_post: {
      name: null,
      id: null,
    },
    err: {
      name: null,
    },
  },
  getters: {
    getbyid: (state) => (id) => {
      return state.content_exame.filter((e) => e.id == id);
    },
  },
  mutations: {
    set_value: (state, value) => {
      state.content_exame = value;
    },
  },
  actions: {
    async get_all({ commit }) {
      const data = await axios.get("http://localhost:3000/content_exame");
      commit("set_value", data.data);
    },
    async create({ commit, state }) {
      console.log(commit);
      await axios.post("http://localhost:3000/content_exame", state.data_post);
    },
    async update({ commit, state }) {
      console.log(commit);
      await axios.put(
        "http://localhost:3000/content_exame/" + state.data_post.id,
        state.data_post
      );
    },
    async delete(context, id) {
      await axios.delete("http://localhost:3000/content_exame/" + id);
      const data = await axios.get("http://localhost:3000/content_exame");
      context.commit("set_value", data.data);
    },
  },
};
export default acounts;
